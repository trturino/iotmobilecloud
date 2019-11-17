using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using turino.iot.cloud.Models;

namespace turino.iot.cloud.Commands
{
    public class DeviceStatusCommand : ICommand
    {
        public string Id { get; set; }
        public string Topic { get; set; }
        public string Subject { get; set; }
        public string EventType { get; set; }
        public DateTime EventTime { get; set; }
        public DeviceData Data { get; set; }
        public string DataVersion { get; set; }
        public string MetadataVersion { get; set; }
    }
}