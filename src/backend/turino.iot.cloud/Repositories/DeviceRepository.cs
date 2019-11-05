using Cosmonaut;
using System;
using System.Threading.Tasks;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ICosmosStore<Device> _deviceStore;

        public DeviceRepository(ICosmosStore<Device> deviceStore)
        {
            _deviceStore = deviceStore;
        }

        public Task<Device> GetDevice(Guid deviceId)
        {
            return _deviceStore.FindAsync(deviceId.ToString());
        }
    }
}