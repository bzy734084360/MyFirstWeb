using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bzy.Logger
{
    public class Log
    {
        private ILogger _logger;
        internal Log(ILogger log)
        {
            this._logger = log;
        }

        public void Trace(string strMsg)
        {
            _logger.Trace(strMsg);
        }

        public void Debug(string strMsg)
        {
            _logger.Debug(strMsg);
        }

        public void Info(string strMsg)
        {
            _logger.Info(strMsg);
        }

        public void Warn(string strMsg)
        {
            _logger.Warn(strMsg);
        }

        public void Error(string strMsg)
        {
            _logger.Error(strMsg);
        }

        public void Fatal(string strMsg)
        {
            _logger.Fatal(strMsg);
        }
    }
}
