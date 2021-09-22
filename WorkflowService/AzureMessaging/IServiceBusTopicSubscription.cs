using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowService.AzureMessaging
{
    public interface IServiceBusTopicSubscription
    {
        Task PrepareFiltersAndHandleMessages();
        Task CloseQueueAsync();
        ValueTask DisposeAsync();

    }
}
