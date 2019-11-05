using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Queries
{
    public class GetDevice : ICommand<Device>
    {
        public Guid DeviceId { get; set; }
    }
}