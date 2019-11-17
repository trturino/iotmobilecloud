using System;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Queries
{
    public class GetDeviceQuery : IQuery<Device>
    {
        public Guid DeviceId { get; set; }
    }
}