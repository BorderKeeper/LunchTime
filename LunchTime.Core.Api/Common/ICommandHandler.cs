namespace LunchTime.Core.Api.Common
{
    public interface ICommandHandler<in TCommand>
    {
        void Execute(TCommand command);
    }

    public interface ICommandHandler<in TCommand, out TResult>
    {
        TResult Execute(TCommand command);
    }
}