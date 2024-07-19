using Comtrade.FinanceTrack.Kafka.Adapter.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Comtrade.FinanceTrack.Kafka.Adapter.Config;
using Comtrade.FinanceTrack.Kafka.Adapter.Helper;
using System.IO;
using System.Reflection;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using Comtrade.FinanceTrack.Helper.Error;

namespace Comtrade.FinanceTrack.Kafka.Adapter
{
    public class KafkaConsumer : IKafkaConsumer
    {
        private readonly KafkaAdapterConfig _kafkaAdapterConfig;
        private readonly ILogger _logger;
        public KafkaConsumer(
            IOptions<KafkaAdapterConfig> kafkaAdapterConfig,
            ILogger<KafkaConsumer> logger
            )
        {
            _kafkaAdapterConfig = kafkaAdapterConfig.Value;
            _logger = logger;
        }


        public void Consume(string topicName, Action<ConsumeResult<Ignore, string>> consumeEvent)
        {

            string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string certPath = Path.Combine(outPutDirectory, @"Certificates/kafkaca.pem");
            string caCertPath = Path.Combine(outPutDirectory, @"Certificates/kafkaca.pem");// Path.Combine(outPutDirectory, @"Certificates/development-kafka.pem");

            var conf = new ConsumerConfig
            {
                GroupId = _kafkaAdapterConfig.GroupId,
                BootstrapServers = _kafkaAdapterConfig.KafkaServer,
                SecurityProtocol = SecurityProtocol.Ssl,
                SslCaLocation = caCertPath, 
               // SslCertificateLocation = certPath,
                EnableAutoCommit = true
            };

            _logger.LogInformation("--------------TASK TREAD ID-----------------------");
            _logger.LogInformation("------------ " + Task.CurrentId.ToString() + " -------");
            _logger.LogInformation("--------------------------------------------------");
            _logger.LogInformation("--------------------------------------------------------------------------------------------------");
            _logger.LogInformation("Start Kafka MWSNAME:" + _kafkaAdapterConfig.GroupId + " TOPIC: " + topicName + " consume list start time", DateTime.Now);
            _logger.LogInformation("--------------------------------------------------------------------------------------------------");

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe(topicName);

            CancellationTokenSource cts = new CancellationTokenSource();

            try
            {
                while (true) //Jedan po jedan
                {
                    //throw new KafkaException(ErrorCode.BrokerNotAvailable); //test exception
                    var cr = c.Consume(cts.Token);
                    //var cr = c.Consume(300);

                    //consumeEvent(cr.Message.Value);
                    consumeEvent(cr);
                    _logger.LogInformation("--------------------------------------------------------------------------------------");
                    _logger.LogInformation("KafkaConsumer.Message ", cr != null && cr.Message != null ? cr.Message.Value : string.Empty);
                    _logger.LogInformation("--------------------------------------------------------------------------------------");
                }
            }
            catch (OperationCanceledException ex)
            {
                string message = ErrorHelper.GetAllMessages(ex);
                _logger.LogError("KafkaConsumer.Consume", ex);
                c.Close();
                throw;
            }
            catch (KafkaException ex)
            {
                string message = ErrorHelper.GetAllMessages(ex);
                _logger.LogError("KafkaConsumer.Consume", ex);
                c.Close();
                throw;
            }
            catch (Exception ex)
            {
                string message = ErrorHelper.GetAllMessages(ex);
                _logger.LogError("KafkaConsumer.Consume", ex);
                c.Close();
                throw;
            }
            finally
            {
                c.Close();
            };
        }

       
     

    }
}
