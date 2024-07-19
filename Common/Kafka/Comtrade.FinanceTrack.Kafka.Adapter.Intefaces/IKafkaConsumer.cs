using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Kafka.Adapter.Interfaces
{
    public interface IKafkaConsumer
    {
        void Consume(string topicName, Action<ConsumeResult<Ignore, string>> consumeEvent);

    }
}
