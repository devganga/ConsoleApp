using ConsoleApp.AppSettings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace ConsoleApp.Services
{
    public interface ITempFileService
    {
        Task<string> CreateTempFileAsync();
        Task<string> CreateAsync(string fileName, string fileExtension);
        Task<bool> UpdateAsync(string fileName, string content);
        Task<string> ReadAsync(string fileName);
        Task<bool> DeleteAsync(string fileName);
    }
    public class TempFileService : ITempFileService
    {
        private readonly ILogger<TempFileService> _logger;
        private readonly ApplicationDataOptions _applicationDataOptions;

        public TempFileService(ILogger<TempFileService> logger, IOptions<ApplicationDataOptions> applicationDataOptions)
        {
            _logger = logger;
            _applicationDataOptions = applicationDataOptions.Value;
        }
        public async Task<string> CreateTempFileAsync()
        {
            var watch = Stopwatch.StartNew();
            // Get the full name of the newly created Temporary file. 
            // Note that the GetTempFileName() method actually creates
            // a 0-byte file and returns the name of the created file.
            var fileName = Path.GetTempFileName();
            _logger.LogInformation($"File created : {fileName}");
            // Craete a FileInfo object to set the file's attributes
            FileInfo fileInfo = new FileInfo(fileName);

            // Set the Attribute property of this file to Temporary. 
            // Although this is not completely necessary, the .NET Framework is able 
            // to optimize the use of Temporary files by keeping them cached in memory.
            fileInfo.Attributes = FileAttributes.Temporary;
            _logger.LogInformation($"Set File Attributes Temporary : {fileName}");
            watch.Stop();
            _logger.LogInformation($"Created TempFile '{fileName}' in {watch.ElapsedMilliseconds} ms");
            return await Task.FromResult(fileName);
        }
        public async Task<string> CreateAsync(string fileName, string fileExtension)
        {
            var watch = Stopwatch.StartNew();
            // Get the full name of the newly created Temporary file. 
            // Note that the GetTempFileName() method actually creates
            // a 0-byte file and returns the name of the created file.
            var filePath = Path.Combine(_applicationDataOptions.GetApplicationTempPath(), $"{fileName}.{fileExtension}");
            File.Create(filePath);
            _logger.LogInformation($"File created : {filePath}");
            // Craete a FileInfo object to set the file's attributes
            FileInfo fileInfo = new FileInfo(filePath);

            // Set the Attribute property of this file to Temporary. 
            // Although this is not completely necessary, the .NET Framework is able 
            // to optimize the use of Temporary files by keeping them cached in memory.
            fileInfo.Attributes = FileAttributes.Temporary;
            _logger.LogInformation($"Set File Attributes Temporary : {filePath}");
            watch.Stop();
            _logger.LogInformation($"Created TempFile '{filePath}' in {watch.ElapsedMilliseconds} ms");
            return await Task.FromResult(filePath);
        }
        public async Task<bool> UpdateAsync(string fileName, string content)
        {
            var watch = Stopwatch.StartNew();
            _logger.LogInformation($"updating File {fileName} with content : {content}");
            StreamWriter streamWriter = File.AppendText(fileName);
            streamWriter.WriteLine(content);
            streamWriter.Flush();
            streamWriter.Close();
            _logger.LogInformation($"updated File {fileName} with content : {content}");
            watch.Stop();
            _logger.LogInformation($"updated TempFile '{fileName}' in {watch.ElapsedMilliseconds} ms");
            return await Task.FromResult(true);
        }
        public async Task<string> ReadAsync(string fileName)
        {
            var watch = Stopwatch.StartNew();
            _logger.LogInformation($"Opening File {fileName}");
            StreamReader streamReader = File.OpenText(fileName);
            _logger.LogInformation($"Reading File {fileName}");
            watch.Stop();
            _logger.LogInformation($"Readed TempFile '{fileName}' in {watch.ElapsedMilliseconds} ms");
            return await streamReader.ReadToEndAsync();
        }
        public async Task<bool> DeleteAsync(string fileName)
        {
            var watch = Stopwatch.StartNew();
            _logger.LogInformation($"deleting File {fileName}");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            watch.Stop();
            _logger.LogInformation($"Deleted TempFile '{fileName}' in {watch.ElapsedMilliseconds} ms");
            return await Task.FromResult(true);
        }
    }
}
