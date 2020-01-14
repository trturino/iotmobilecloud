using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAll();

        Task<Device> Get(Guid deviceId);

        Task<Device> GetByName(string deviceName);

        Task Insert(Device device);

        Task Update(Device device);
    }
}