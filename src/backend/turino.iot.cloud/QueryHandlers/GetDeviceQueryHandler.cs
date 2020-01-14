using System.Threading.Tasks;
using turino.iot.cloud.Models;
using turino.iot.cloud.Queries;
using turino.iot.cloud.Repositories;

namespace turino.iot.cloud.QueryHandlers
{
    public class GetDeviceQueryHandler : IQueryHandler<GetDeviceQuery, Device>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDeviceQueryHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<Device> ExecuteAsync(GetDeviceQuery command)
        {
            return _deviceRepository.Get(command.DeviceId);
        }
    }
}