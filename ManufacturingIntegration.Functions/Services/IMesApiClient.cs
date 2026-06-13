using Shared.Contracts.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManufacturingIntegration.Functions.Services
{
    public interface IMesApiClient
    {
        Task CreateJobAsync(ProductionOrderReleasedEvent productionOrder);
    }
}
