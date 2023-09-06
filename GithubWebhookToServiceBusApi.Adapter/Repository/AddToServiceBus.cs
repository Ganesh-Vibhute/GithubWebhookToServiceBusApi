using Azure.Messaging.ServiceBus;
using Azure.Security.KeyVault.Secrets;
using GithubWebhookToServiceBusApi.Adapter.Contracts;
using GithubWebhookToServiceBusApi.Adapter.KeyVault;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GithubWebhookToServiceBusApi.Adapter.Repository
{
    public class AddToServiceBus : IRepositories
    {
       
        public async Task<string> SendToTopic(dynamic topic)
        {
            ServiceBusSecrets secrets = new ServiceBusSecrets();
            try
            {
                var ConnectionString = secrets.GetServicebusConnectionString();
                var TopicName = secrets.GetTopicName();
                ServiceBusClient serviceBusClient = new ServiceBusClient(ConnectionString);
                ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(TopicName);
                ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
                ServiceBusMessage serviceBusMessage = new ServiceBusMessage(JsonConvert.SerializeObject(topic,Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    }));
                serviceBusMessage.ContentType = "application/json";
                serviceBusMessageBatch.TryAddMessage(serviceBusMessage);
                await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);
                await serviceBusSender.DisposeAsync();
                await serviceBusClient.DisposeAsync();
                return "Data Send To Topic";
            }

            catch (Exception ex)
            {

                return "Something Went Wrong";

            }
            
        }
    }

}
