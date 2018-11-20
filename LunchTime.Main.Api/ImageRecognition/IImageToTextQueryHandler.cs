using System.Threading.Tasks;
using LunchTime.Core.Api.Common;
using LunchTime.Main.Api.ImageRecognition.Queries;

namespace LunchTime.Main.Api.ImageRecognition
{
    public interface IImageToTextQueryHandler : IQueryHandler<ImageToTextQuery, Task<string>>
    {        
    }
}