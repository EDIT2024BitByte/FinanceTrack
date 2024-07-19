using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Comtrade.FinanceTrack.Helper.Error;
//using Comtrade.FinanceTrack.Common.Kafka;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Comtrade.FinanceTrack.Kafka.Adapter.Helper;
using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Comtrade.FinanceTrack.Kafka.Adapter
{
    public class KafkaProducer<T> : IKafkaProducer<T> where T : class
    {
        private readonly KafkaAdapterConfig _kafkaAdapterConfig;
        private readonly ILogger _logger;
        public KafkaProducer(
            IOptions<KafkaAdapterConfig> kafkaAdapterConfig,
            ILogger<KafkaProducer<T>> logger
            )
        {
            _kafkaAdapterConfig = kafkaAdapterConfig.Value;
            _logger = logger;
        }

        public async Task<string> Produce(T t, string topic)
        {
            string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string certPath = Path.Combine(outPutDirectory, @"Certificates/kafkaca.pem");
            string caCertPath = Path.Combine(outPutDirectory, @"Certificates/kafkaca.pem"); //Path.Combine(outPutDirectory, @"Certificates/development-kafka.pem");

            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaAdapterConfig.KafkaServer,
                SecurityProtocol = SecurityProtocol.Ssl,
                SslCaLocation = caCertPath,
                SslCertificateLocation = certPath
            };

            using var p = new ProducerBuilder<Null, string>(config).SetErrorHandler((producer, error) =>
            {
                _logger.LogError("KafkaProducer Produce error: ", producer);
            }).Build();

            try
            {
                var message = new Message<Null, string>();
                string JsonString = JsonSerializer.Serialize(t);//JsonHelper.JsonSerializer<T>(t);
                message.Value = JsonString;
                var dr = await p.ProduceAsync(topic, message);

                return dr.Status.ToString();
            }
            catch (ProduceException<Null, string> ex)
            {
                string message = ErrorHelper.GetAllMessages(ex);
                _logger.LogError(message, "KafkaProducer Produce ProduceException: ");
                throw;
            }
            catch (Exception ex)
            {
                string message = ErrorHelper.GetAllMessages(ex);
                _logger.LogError(message, "KafkaProducer Produce error: ");
                throw;
            }
        }



    }
}
