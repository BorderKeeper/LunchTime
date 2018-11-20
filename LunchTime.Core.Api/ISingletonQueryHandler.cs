namespace LunchTime.Core.Api
{
    public interface ISingletonQueryHandler<in TQuery, out TResult>
    {
        TResult Execute(TQuery query);
    }
}