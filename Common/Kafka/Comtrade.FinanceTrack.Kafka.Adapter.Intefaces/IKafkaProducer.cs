using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Kafka.Adapter.Interfaces
{
    public interface IKafkaProducer<T> where T : class
    {
        Task<string> Produce(T t, string topic);
    }
}
