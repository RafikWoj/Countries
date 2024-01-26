using Countries.Interfaces;
using Countries.Logger;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Countries.Services
{
    public class LoggingService : ILoggingService
    {
        private MyLogger _logger;

        public LoggingService(MyLogger logger)
        {
            _logger = logger;
        }

        public void LoggError(string info, Exception ex)
        {
            _logger.LogError(info, ex);
        }

        public void LoggInfo(string info)
        {
            _logger.LogInformation(info);
        }
    }
}