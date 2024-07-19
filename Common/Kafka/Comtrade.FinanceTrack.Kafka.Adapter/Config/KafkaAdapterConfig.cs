using System;
using System.Collections.Generic;
using System.Text;

namespace Comtrade.FinanceTrack.Kafka.Adapter.Config
{
    public class KafkaAdapterConfig
    {
        public string GroupId { get; set; }
        public string KafkaServer { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RestMethod { get; set; }
        public int KafkaConsumeWaitInterval { get; set; }
    }
}
