using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using turino.iot.cloud.CommandHandler;
using turino.iot.cloud.Commands;

namespace turino.iot.cloud.Functions
{
    public static class DeviceStatus
    {
        [FunctionName("DeviceStatus")]
        public static async Task Run([ServiceBusTrigger("iot-events", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            try
            {
                var services = Startup.GetServices();
                var commandHandler = (DeviceStatusCommandHandler)services.GetService(typeof(DeviceStatusCommandHandler));
                var command = JsonConvert.DeserializeObject<DeviceStatusCommand>(myQueueItem);

                await commandHandler.ExecuteAsync(command);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}