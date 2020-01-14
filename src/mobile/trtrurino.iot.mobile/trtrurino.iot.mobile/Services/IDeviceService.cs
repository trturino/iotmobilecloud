using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using trtrurino.iot.mobile.Models;

namespace trtrurino.iot.mobile.Services
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetDevices();

        Task SetStatus(Guid deviceId, bool value);
    }
}