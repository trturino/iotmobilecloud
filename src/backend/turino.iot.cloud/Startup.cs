using Cosmonaut;
using Cosmonaut.Extensions.Microsoft.DependencyInjection;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using turino.iot.cloud.CommandHandler;
using turino.iot.cloud.QueryHandlers;
using turino.iot.cloud.Repositories;
using Device = turino.iot.cloud.Models.Device;

namespace turino.iot.cloud
{
    public static class Startup
    {
        public static IServiceProvider GetServices()
        {
            var serviceCollection = new ServiceCollection();

            var cosmosDbUri = Environment.GetEnvironmentVariable("CosmosDbUri");
            var cosmosDbKey = Environment.GetEnvironmentVariable("CosmosDbKey");
            var iotHubConnectionString = Environment.GetEnvironmentVariable("IotHubConnectionString");
            var cosmosSettings = new CosmosStoreSettings("turino-iot", new Uri(cosmosDbUri), cosmosDbKey);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            serviceCollection.AddCosmosStore<Device>(cosmosSettings);
            serviceCollection.AddTransient<DeviceStatusCommandHandler>();
            serviceCollection.AddTransient<GetDevicesQueryHandler>();
            serviceCollection.AddTransient<GetDeviceQueryHandler>();
            serviceCollection.AddTransient<SwitchDeviceLightCommandHandler>();
            serviceCollection.AddTransient(s =>
                ServiceClient.CreateFromConnectionString(iotHubConnectionString));

            serviceCollection.AddTransient<IDeviceRepository, DeviceRepository>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}