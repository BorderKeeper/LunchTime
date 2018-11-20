using LunchTime.Core.Api.Common;
using LunchTime.Core.Api.Common.Enums;
using LunchTime.Main.Api.Retriever.Commands;

namespace LunchTime.Main.Api.Retriever.CommandHandlers
{
    public interface IDeleteRestaurantCommandHandler : ICommandHandler<DeleteRestaurantCommand, DeletionStatus>
    {
    }
}