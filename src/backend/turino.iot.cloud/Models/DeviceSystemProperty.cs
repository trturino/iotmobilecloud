using Newtonsoft.Json;
using System;

public class DeviceSystemProperty
{
    [JsonProperty("iothub-connection-device-id")]
    public string DeviceId { get; set; }

    [JsonProperty("iothub-connection-auth-method")]
    public string AuthMethod { get; set; }

    [JsonProperty("iothub-connection-auth-generation-id")]
    public string AuthGenerationId { get; set; }

    [JsonProperty("iothub-enqueuedtime")]
    public DateTime EnqueuedTime { get; set; }

    [JsonProperty("iothub-message-source")]
    public string MessageSource { get; set; }
}