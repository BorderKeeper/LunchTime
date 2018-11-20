using System.Threading.Tasks;
using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.Retriever.Commands;

namespace LunchTime.Main.Api.Retriever.CommandHandlers
{
    public interface IUpdateRestaurantCommandHandler : ICommandHandler<UpdateRestaurantCommand, Task>
    {        
    }
}