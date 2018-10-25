using System;

namespace CLWebApp.Models
{
    public class ErrorViewModel
    {
        internal string boxOilingQuantity;

        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}