using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowService.AzureMessaging.Dtos
{
    public class IntegrationBaseMessageDto
    {
        public Guid ID { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
