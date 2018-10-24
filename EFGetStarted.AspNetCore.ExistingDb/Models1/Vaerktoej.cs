using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models1
{
    public partial class Vaerktoej
    {
        public long VaerktoejsId { get; set; }
        public DateTime Anskaffet { get; set; }
        public string Fabrikat { get; set; }
        public string Model { get; set; }
        public string Serienr { get; set; }
        public string Type { get; set; }
        public int? LiggerIvtk { get; set; }

        public Vaerktoejskasse LiggerIvtkNavigation { get; set; }
    }
}
