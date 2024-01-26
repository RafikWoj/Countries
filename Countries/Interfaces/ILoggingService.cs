using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Countries.Interfaces
{
    public interface ILoggingService
    {
        void LoggInfo(string info);
        void LoggError(string info, Exception ex);
    }
}
