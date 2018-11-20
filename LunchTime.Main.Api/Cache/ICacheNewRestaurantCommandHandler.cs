using System.Threading.Tasks;
using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.Cache.Commands;

namespace LunchTime.Main.Api.Cache
{
    public interface ICacheNewRestaurantCommandHandler : ICommandHandler<CacheNewRestaurantCommand, Task>
    {       
    }
}