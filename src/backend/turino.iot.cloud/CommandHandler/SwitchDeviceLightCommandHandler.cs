﻿using Microsoft.Azure.Devices;
using System.Text;
using System.Threading.Tasks;
using turino.iot.cloud.Commands;
using turino.iot.cloud.Repositories;

namespace turino.iot.cloud.CommandHandler
{
    public class SwitchDeviceLightCommandHandler : ICommandHandler<SwitchDeviceLightCommand>
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

        public async Task ExecuteAsync(SwitchDeviceLightCommand command)
        {
            var device = await _deviceRepository.Get(command.DeviceId);
            if (command.Value)
            {
                await SendDeviceMessage(device.DeviceName, "1");
            }
            else
            {
                await SendDeviceMessage(device.DeviceName, "0");
            }

            device.Status = command.Value;
            await _deviceRepository.Update(device);
        }

        private async Task SendDeviceMessage(string deviceName, string message)
        {
            await _serviceClient.SendAsync(deviceName, new Message(Encoding.ASCII.GetBytes(message)));
        }
    }
}