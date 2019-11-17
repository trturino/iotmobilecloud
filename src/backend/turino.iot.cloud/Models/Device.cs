using System;

namespace turino.iot.cloud.Models
{
    public class Device
    {
        public Guid DeviceId { get; set; }

        public string DeviceName { get; set; }

        public bool Status { get; set; }
    }
}