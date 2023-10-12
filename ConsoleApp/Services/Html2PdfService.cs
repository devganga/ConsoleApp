using ConsoleApp.AppSettings;
using iText.Html2pdf;
using iText.StyledXmlParser.Css.Media;
using Microsoft.Extensions.Options;

namespace ConsoleApp.Services
{
    public interface IHtml2PdfService
    {
        Task ConvertToPdf(string sourceHtmlFileName, string destinationPdfFileName);
    }

    public class Html2PdfService : IHtml2PdfService
    {
        private readonly ConverterProperties _converterProperties;
        private readonly ApplicationDataOptions _options;
        public Html2PdfService(IOptions<ApplicationDataOptions> options)
        {
            _converterProperties = new ConverterProperties();
            _converterProperties.SetMediaDeviceDescription(new MediaDeviceDescription(MediaType.PRINT));
            _options = options.Value;
        }
        public Task ConvertToPdf(string sourceHtmlFileName, string destinationPdfFileName)
        {
            var ApplicationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), _options.FolderName ?? "ConsoleApp");
            FileInfo htmlFileInfo = new(sourceHtmlFileName);
            FileInfo pdfFileInfo = new(Path.Combine(ApplicationFolder, destinationPdfFileName));

            HtmlConverter.ConvertToPdf(htmlFileInfo, pdfFileInfo, _converterProperties);
            //HtmlConverter.ConvertToPdf(htmlFileInfo, pdfFileInfo);
            return Task.CompletedTask;
        }
    }
}

