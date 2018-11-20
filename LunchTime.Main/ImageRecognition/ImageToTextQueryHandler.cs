using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using IronOcr;
using LunchTime.Main.Api.ImageRecognition;
using LunchTime.Main.Api.ImageRecognition.Queries;

namespace LunchTime.Main.ImageRecognition
{
    public class ImageToTextQueryHandler : IImageToTextQueryHandler
    {
        public async Task<string> Execute(ImageToTextQuery query)
        {
            var localFile = await StorePdfLocally(query.Uri);

            var ocr = new AdvancedOcr
            {
                CleanBackgroundNoise = false,
                EnhanceContrast = true,
                EnhanceResolution = false,
                Language = IronOcr.Languages.Czech.OcrLanguagePack,
                Strategy = AdvancedOcr.OcrStrategy.Fast,
                ColorSpace = AdvancedOcr.OcrColorSpace.GrayScale,
                DetectWhiteTextOnDarkBackgrounds = false,
                InputImageType = AdvancedOcr.InputTypes.Document,
                RotateAndStraighten = false,
                ReadBarCodes = false,
                ColorDepth = 4
            };

            var result = ocr.Read(localFile);
            
            File.Delete(localFile);

            return FormatResult(result);
        }

        private async Task<string> StorePdfLocally(Uri uri)
        {
            var extension = uri.ToString().Split('.').Last();
            var tempPath = Path.GetTempPath() + Guid.NewGuid() + "." + extension;

            using (var webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(uri, tempPath);
            }

            return tempPath;
        }

        private string FormatResult(OcrResult convertedImage)
        {
            var result = new StringBuilder();

            foreach (OcrResult.OcrPage page in convertedImage.Pages)
            {
                ProcessPage(page, result);
            }

            return result.ToString();
        }

        private void ProcessPage(OcrResult.OcrPage page, StringBuilder result)
        {
            foreach (OcrResult.OcrLine line in page.LinesOfText)
            {
                ProcessLine(line, result);
            }
        }

        private void ProcessLine(OcrResult.OcrLine line, StringBuilder result)
        {
            foreach (OcrResult.OcrWord word in line.Words)
            {
                ProcessWord(word, result);
            }

            result.Append("<br>");
        }

        private void ProcessWord(OcrResult.OcrWord word, StringBuilder result)
        {
            if (word.FontIsBold)
            {
                result.Append($"<b>{word.Text}</b>");
            }
            else if(word.FontIsItalic)
            {
                result.Append($"<i>{word.Text}</i>");
            }
            else
            {
                result.Append(word.Text);
            }            
        }
    }
}