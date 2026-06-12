using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Event
{
    public record ProductionOrderReleasedEvent(
    string OrderNumber,
    string ProductCode,
    int Quantity,
    DateTime ReleasedAt);
}
