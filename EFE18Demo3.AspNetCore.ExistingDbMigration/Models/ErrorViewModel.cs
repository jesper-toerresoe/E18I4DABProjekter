using System;

namespace EFE18Demo3.AspNetCore.ExistingDbMigration.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}