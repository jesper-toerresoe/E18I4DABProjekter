using System;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}