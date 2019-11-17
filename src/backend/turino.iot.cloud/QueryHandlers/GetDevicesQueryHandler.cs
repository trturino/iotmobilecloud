using System.Collections.Generic;
using System.Threading.Tasks;
using turino.iot.cloud.Models;
using turino.iot.cloud.Queries;
using turino.iot.cloud.Repositories;

namespace turino.iot.cloud.QueryHandlers
{
    public class GetDevicesQueryHandler : IQueryHandler<GetDevices, IEnumerable<Device>>
    {
        private readonly IDeviceRepository _deviceRepository;

        public GetDevicesQueryHandler(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public Task<IEnumerable<Device>> ExecuteAsync(GetDevices query)
        {
            return _deviceRepository.GetAll();
        }
    }
}