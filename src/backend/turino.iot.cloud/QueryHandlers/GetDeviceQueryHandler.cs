using AzureFromTheTrenches.Commanding.Abstractions;
using System;
using System.Threading.Tasks;
using turino.iot.cloud.Models;
using turino.iot.cloud.Queries;
using turino.iot.cloud.Repositories;

namespace turino.iot.cloud.QueryHandlers
{
    public class GetDeviceQueryHandler : ICommandHandler<GetDevice, Device>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceQueryHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<Device> ExecuteAsync(GetDevice command, Device previousResult)
        {
            if (command.DeviceId == Guid.Empty)
                return null;

            return _deviceRepository.GetDevice(command.DeviceId);
        }
    }
}`