using Countries.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Countries.Logger
{
    /// <summary>
    /// Empty box....
    /// Instead of this, we can use third party logger i.e. log4Net
    /// </summary>
    public class MyLogger
    {
        
        public void LogError(string info, Exception ex)
        {
            ;
        }

        public void LogInformation(string info)
        {
            ;
        }
    }
}