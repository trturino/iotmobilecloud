using Cosmonaut.Attributes;

namespace turino.iot.cloud.Models
{
    public class Device
    {
        [CosmosPartitionKey]
        public string Id { get; set; }

        public string DeviceName { get; set; }

        public bool Status { get; set; }
    }
}