using AzureFromTheTrenches.Commanding.Abstractions;
using System;

namespace turino.iot.cloud.Commands
{
    public class SwitchDeviceLight : ICommand
    {
        public Guid DeviceId { get; set; }

        public bool Value { get; set; }
    }
}