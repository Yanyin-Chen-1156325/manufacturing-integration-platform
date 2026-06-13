using Shared.Contracts.Event;
using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ManufacturingIntegration.Functions.Services
{
    public class MesApiClient : IMesApiClient
    {
        private readonly HttpClient _httpClient;

        public MesApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateJobAsync(ProductionOrderReleasedEvent order)
        {
            var request = new
            {
                JobNumber = order.OrderNumber,
                ProductCode = order.ProductCode,
                PlannedQuantity = order.Quantity
            };

            var response = await _httpClient.PostAsJsonAsync(
               "/api/jobs",
               request);

            response.EnsureSuccessStatusCode();
        }
    }
}
