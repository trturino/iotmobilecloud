using Cosmonaut;
using Cosmonaut.Extensions;
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

        public Task<Device> GetDeviceByName(string deviceName)
        {
            return _deviceStore.Query().FirstOrDefaultAsync(x => x.DeviceName == deviceName);
        }

        public Task Insert(Device device)
        {
            return _deviceStore.AddAsync(device);
        }

        public Task Update(Device device)
        {
            return _deviceStore.UpdateAsync(device);
        }
    }
}