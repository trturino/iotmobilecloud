using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using FunctionMonkey.Abstractions;
using FunctionMonkey.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using turino.iot.cloud.Commands;
using turino.iot.cloud.Models;

namespace turino.iot.cloud
{
    public class FunctionAppConfiguration : IFunctionAppConfiguration
    {
        private const string QueueName = "iot-events";
        private const string ServiceBusConnectionName = "serviceBusConnection";

        public void Build(IFunctionHostBuilder builder)
        {
            builder.Setup((serviceCollection, commandRegistry) =>
                {
                    var cosmosDbUri = Environment.GetEnvironmentVariable("CosmosDbUri");
                    var cosmosDbKey = Environment.GetEnvironmentVariable("CosmosDbKey");
                    var cosmosSettings = new CosmosStoreSettings("turino-iot", new Uri(cosmosDbUri), cosmosDbKey);

                    JsonConvert.DefaultSettings = () => new JsonSerializerSettings
                    {
                        Formatting = Formatting.Indented,
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    serviceCollection.AddCosmosStore<Device>(cosmosSettings);
                    serviceCollection
                        .AddLogging();
                })
                .Functions(functions => functions
                    .ServiceBus(ServiceBusConnectionName, serviceBus => serviceBus
                        .QueueFunction<DeviceStatusCommand>(QueueName)
                    )
                );
        }
    }
}