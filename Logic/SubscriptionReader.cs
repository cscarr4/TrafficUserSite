using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrafficUserSite.Logic
{
    public class SubscriptionReader : IAsyncDisposable
    {
        private readonly ILogger<SubscriptionReader> logger;
        private readonly IHubContext<StatusHub> hub;
        private readonly ServiceBusProcessor processor;

        public SubscriptionReader(
            IConfiguration configuration, 
            ILogger<SubscriptionReader> logger, 
            ServiceBusClient client,
            IHubContext<StatusHub> hub
            )
        {
            this.logger = logger;
            this.hub = hub;
            var topicName = configuration["TopicName"];
            var subscriptionName = configuration["SubscriptionName"];
            processor = client.CreateProcessor(topicName, subscriptionName);
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
        }

        public async Task StartReading()
        {
            if (processor.IsProcessing)
                return;
            logger.LogInformation("Start processing subscription");
            await processor.StartProcessingAsync();
        }

        public async Task StopReading()
        {
            if (!processor.IsProcessing)
                return;
            logger.LogInformation("Stop processing subscription");
            await processor.StopProcessingAsync();
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            var message = args.Message.Body.ToString();

            // Inject a Processed timestamp into the json object
            var processedProperty = $"\"Processed\": \"{DateTime.UtcNow}\"";
            var index = message.LastIndexOf('}');
            var injectedMessage = message.Insert(index, $", {processedProperty}");

            logger.LogInformation(injectedMessage);
            await hub.Clients.All.SendAsync("ReceiveMessage", injectedMessage);
        }

        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            logger.LogError(args.Exception, "An unexpected error occurred");
            await Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            processor.ProcessMessageAsync -= MessageHandler;
            processor.ProcessErrorAsync -= ErrorHandler;
            await processor.DisposeAsync();
        }
    }
}
