using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using turino.iot.cloud.CommandHandler;
using turino.iot.cloud.Commands;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Functions
{
    public static class SwitchLight
    {
        [FunctionName("SwitchLight")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "devices/{deviceId}/status")] HttpRequest req,
            string deviceId,
            ILogger log)
        {
            var services = Startup.GetServices();
            var queryHandler = (SwitchDeviceLightCommandHandler)services.GetService(typeof(SwitchDeviceLightCommandHandler));
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Message>(requestBody);

            await queryHandler.ExecuteAsync(new SwitchDeviceLightCommand { DeviceId = new Guid(deviceId), Value = data.IsOn == 1 });

            return new OkObjectResult(null);
        }
    }
}