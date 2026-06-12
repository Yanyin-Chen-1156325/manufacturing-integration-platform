using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mes.Api.Dto;
using Mes.Api.Model;
using Mes.Api.Data;

namespace Mes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly MesDbContext _dbContext;
        private readonly ILogger<JobsController> _logger;

        public JobsController(MesDbContext dbContext, ILogger<JobsController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Create a new job.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(JobRequest request)
        {
            var job = new Job
            {
                JobNumber = $"JOB-{DateTime.UtcNow:yyyyMMddHHmmss}",
                ProductCode = request.ProductCode,
                PlannedQuantity = request.PlannedQuantity,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Jobs.Add(job);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation("Job created. JobNumber: {JobNumber}",job.JobNumber);
            return CreatedAtAction(nameof(GetById), new { id = job.Id }, MapToResponse(job));
        }

        [HttpGet]
        public async Task<ActionResult<List<JobResponse>>> GetAll()
        {
            var jobs = await _dbContext.Jobs
                .ToListAsync();

            var response = jobs.Select(MapToResponse).ToList();

            return Ok(response);
        }

        /// <summary>
        /// Get a job by ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobResponse>> GetById(int id)
        {
            var job = await _dbContext.Jobs.FindAsync(id);

            if (job == null)
            {
                return NotFound();
            }

            return Ok(MapToResponse(job));
        }

        /// <summary>
        /// Map Job entity to JobResponse DTO.
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        private static JobResponse MapToResponse(Job job)
        {
            return new JobResponse
            {
                Id = job.Id,
                JobNumber = job.JobNumber,
                ProductCode = job.ProductCode,
                PlannedQuantity = job.PlannedQuantity,
                CreatedAt = job.CreatedAt
            };
        }
    }
}
