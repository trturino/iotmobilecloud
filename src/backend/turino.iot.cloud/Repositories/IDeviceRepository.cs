using System;
using System.Threading.Tasks;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device> GetDevice(Guid deviceId);

        Task<Device> GetDeviceByName(string deviceName);

        Task Insert(Device device);

        Task Update(Device device);
    }
}