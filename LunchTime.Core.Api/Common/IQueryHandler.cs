namespace LunchTime.Core.Api.Common
{
    public interface IQueryHandler<in TQuery, out TResult>
    {
        TResult Execute(TQuery query);
    }
}