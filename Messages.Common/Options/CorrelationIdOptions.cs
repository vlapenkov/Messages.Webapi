using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messages.Common.Options
{
    public class CorrelationIdOptions
    {
        private const string _defaultHeader = "X-Correlation-ID";

        /// <summary>
        /// The header field name where the correlation ID will be stored
        /// </summary>
        public string Header { get; set; } = _defaultHeader;

        /// <summary>
        /// Controls whether the correlation ID is returned in the response headers
        /// </summary>
        public bool IncludeInResponse { get; set; } = true;
    }
}
