using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using turino.iot.cloud.Queries;
using turino.iot.cloud.QueryHandlers;

namespace turino.iot.cloud.Functions
{
    public static class GetDevice
    {
        [FunctionName("GetDeviceDetails")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "devices/{deviceId}")] HttpRequest req,
            string deviceId,
            ILogger log)
        {
            var services = Startup.GetServices();
            var queryHandler = (GetDeviceQueryHandler)services.GetService(typeof(GetDeviceQueryHandler));

            var devices = await queryHandler.ExecuteAsync(new GetDeviceQuery() { DeviceId = new Guid(deviceId) });

            return new OkObjectResult(devices);
        }
    }
}