using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using turino.iot.cloud.Commands;
using turino.iot.cloud.Models;
using turino.iot.cloud.Repositories;

namespace turino.iot.cloud.CommandHandler
{
    public class DeviceStatusCommandHandler : ICommandHandler<DeviceStatusCommand>
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceStatusCommandHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task ExecuteAsync(DeviceStatusCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Data.Body))
                return;

            var device = await _deviceRepository.GetByName(command.Data.SystemProperties.DeviceId);
            var data = Convert.FromBase64String(command.Data.Body);
            var messageBody = Encoding.ASCII.GetString(data);
            var message = JsonConvert.DeserializeObject<Message>(messageBody);

            if (device == null)
            {
                device = new Device();
                device.Id = Guid.NewGuid().ToString();
                device.DeviceName = command.Data.SystemProperties.DeviceId;
                device.Status = message.IsOn == 1;
                await _deviceRepository.Insert(device);

                return;
            }

            device.Status = message.IsOn == 1;
            await _deviceRepository.Update(device);
        }
    }
}