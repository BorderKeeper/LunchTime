using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.ImageRecognition.Queries;

namespace LunchTime.Main.Api.ImageRecognition
{
    public interface IConvertToTextQueryHandler : IQueryHandler<ConvertToTextQuery, string>
    {        
    }
}