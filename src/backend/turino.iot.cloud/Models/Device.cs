using System;
using System.Collections.Generic;

namespace turino.iot.cloud.Models
{
    public class Device
    {
        public Device()
        {
            Statuses = new List<DeviceStatus>();
        }

        public Guid DeviceId { get; set; }
        public string DeviceName { get; set; }

        public IList<DeviceStatus> Statuses { get; set; }
    }
}