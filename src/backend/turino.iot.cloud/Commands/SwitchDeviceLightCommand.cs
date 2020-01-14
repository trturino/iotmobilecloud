using System;

namespace turino.iot.cloud.Commands
{
    public class SwitchDeviceLightCommand : ICommand
    {
        public Guid DeviceId { get; set; }

        public bool Value { get; set; }
    }
}