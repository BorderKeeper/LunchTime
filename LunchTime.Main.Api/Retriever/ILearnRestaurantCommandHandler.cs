using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.Retriever.Commands;

namespace LunchTime.Main.Api.Retriever
{
    public interface ILearnRestaurantCommandHandler : ICommandHandler<LearnRestaurantCommand, int>
    {        
    }
}