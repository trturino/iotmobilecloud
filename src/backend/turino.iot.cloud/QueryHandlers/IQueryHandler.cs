using System.Threading.Tasks;
using turino.iot.cloud.Queries;

namespace turino.iot.cloud.QueryHandlers
{
    public interface IQueryHandler<in T, T1> where T : IQuery<T1>
    {
        Task<T1> ExecuteAsync(T command);
    }
}