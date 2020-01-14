using Cosmonaut;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<Device>> GetAll()
        {
            return _deviceStore.QueryMultipleAsync("SELECT * FROM c");
        }

        public Task<Device> Get(Guid deviceId)
        {
            return _deviceStore.FindAsync(deviceId.ToString());
        }

        public async Task<Device> GetByName(string deviceName)
        {
            return await _deviceStore.QuerySingleAsync($"SELECT * FROM c WHERE c.deviceName = \"{deviceName}\"");
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