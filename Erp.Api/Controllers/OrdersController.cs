using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Erp.Api.Dto;
using Erp.Api.Model;
using Erp.Api.Data;
using Shared.Contracts.Event;
using Erp.Api.Services;

namespace Erp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ErpDbContext _dbContext;
        private readonly ILogger<OrdersController> _logger;
        private readonly IServiceBusPublisher _publisher;

        public OrdersController(ErpDbContext dbContext, ILogger<OrdersController> logger, IServiceBusPublisher publisher)
        {
            _dbContext = dbContext;
            _logger = logger;
            _publisher = publisher;
        }

        /// <summary>
        /// Create a new order with status "Draft". 
        /// The order number is generated based on the current timestamp to ensure uniqueness.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<OrderRequest>> Create(OrderRequest request) 
        {
            var order = new Order
            {
                OrderNumber = $"PO-{DateTime.UtcNow:yyyyMMddHHmmss}",
                ProductCode = request.ProductCode,
                Quantity = request.Quantity,
                Status = "Draft",
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Order created. OrderNumber: {OrderNumber}", order.OrderNumber);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, MapToResponse(order));

        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetAll()
        {
            var orders = await _dbContext.Orders
                .ToListAsync();

            var response = orders.Select(MapToResponse).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Get order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponse>> GetById(int id)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(MapToResponse(order));
        }

        /// <summary>
        /// Release an order. Only orders in "Draft" status can be released.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/release")]
        public async Task<ActionResult<OrderResponse>> Release(int id)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            if (order.Status != "Draft")
            {
                return BadRequest($"Only orders in 'Draft' status can be released. Id:[{order.Id}], Status:[{order.Status}].");
            }

            order.Status = "Released";
            order.ReleasedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            var evt = new ProductionOrderReleasedEvent(
                order.OrderNumber,
                order.ProductCode,
                order.Quantity,
                order.ReleasedAt!.Value);

            await _publisher.PublishAsync(evt);

            _logger.LogInformation(
                "Production Order Released {@Event}",
                evt);

            return NoContent();
        }

        /// <summary>
        /// Maps an Order entity to an OrderResponse DTO.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        private static OrderResponse MapToResponse(Order order)
        {
            return new OrderResponse
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                ProductCode = order.ProductCode,
                Quantity = order.Quantity,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                ReleasedAt = order.ReleasedAt
            };
        }
    }
}
