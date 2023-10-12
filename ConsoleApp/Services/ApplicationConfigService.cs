using ConsoleApp.AppSettings;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Services
{

    public interface IApplicationConfigService
    {
        ApplicationDataOptions ApplicationDataOptions { get; }
    }

    public class ApplicationConfigService : IApplicationConfigService
    {
        private readonly ApplicationDataOptions _applicationDataOptions;

        public ApplicationConfigService(IOptions<ApplicationDataOptions> applicationDataOptions)
        {
            _applicationDataOptions = applicationDataOptions.Value;
        }

        public ApplicationDataOptions ApplicationDataOptions { get { return _applicationDataOptions; } }

    }
}
