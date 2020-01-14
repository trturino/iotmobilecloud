using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using trtrurino.iot.mobile.Models;

namespace trtrurino.iot.mobile.Services
{
    public class DeviceService : IDeviceService
    {
        private const string BackendUrl = "https://turino-iot.azurewebsites.net/api";

        public async Task<IEnumerable<Device>> GetDevices()
        {
            var client = new HttpClient();
            var payload = await client.GetStringAsync($"{BackendUrl}/devices");
            var devices = JsonConvert.DeserializeObject<IEnumerable<Device>>(payload);

            return devices;
        }

        public async Task SetStatus(Guid deviceId, bool value)
        {
            var message = new Message { IsOn = value ? 1 : 0 };
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(message);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await client.PostAsync($"{BackendUrl}/devices/{deviceId}/status", data);
        }
    }
}