using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using turino.iot.cloud.Queries;
using turino.iot.cloud.QueryHandlers;

namespace turino.iot.cloud.Functions
{
    public static class GetAllDevices
    {
        [FunctionName("GetAllDevices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "devices")] HttpRequest req,
            ILogger log)
        {
            var services = Startup.GetServices();
            var queryHandler = (GetDevicesQueryHandler)services.GetService(typeof(GetDevicesQueryHandler));

            var devices = await queryHandler.ExecuteAsync(new GetDevices());

            return new OkObjectResult(devices);
        }
    }
}