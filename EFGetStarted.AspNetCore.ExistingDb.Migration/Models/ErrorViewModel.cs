using System;

namespace EFGetStarted.AspNetCore.ExistingDb.Migration.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}