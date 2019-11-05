using System.Text;
using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.Azure.Devices;
using System.Threading.Tasks;
using turino.iot.cloud.Commands;
using turino.iot.cloud.Repositories;

namespace turino.iot.cloud.CommandHandler
{
    public class SwitchDeviceLightCommandHandler : ICommandHandler<SwitchDeviceLight>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ServiceClient _serviceClient;

        public SwitchDeviceLightCommandHandler(
            IDeviceRepository deviceRepository,
            ServiceClient serviceClient
            )
        {
            _deviceRepository = deviceRepository;
            _serviceClient = serviceClient;
        }

        public async Task ExecuteAsync(SwitchDeviceLight command)
        {
            var device = await _deviceRepository.GetDevice(command.DeviceId);
            if (command.Value)
            {
                await SendDeviceMessage(device.DeviceName, "A");
            }
            else
            {
                await SendDeviceMessage(device.DeviceName, "B");
            }
        }

        private async Task SendDeviceMessage(string deviceName, string message)
        {
            await _serviceClient.SendAsync(deviceName, new Message(Encoding.ASCII.GetBytes(message)));
        }
    }
}