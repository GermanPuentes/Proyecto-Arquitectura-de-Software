using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriptor.Config
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }
        public string TopicName { get; set; }
        public string SubscriptionName { get; set; }
    }
}
