using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDbMigration.Models
{
    public partial class Vaerktoej
    {
        public long VTId { get; set; }
        public DateTime VTAnskaffet { get; set; }
        public string VTFabrikat { get; set; }
        public string VTModel { get; set; }
        public string VTSerienr { get; set; }
        public string VTType { get; set; }
        public int? LiggerIvtk { get; set; }

        public Vaerktoejskasse LiggerIvtkNavigation { get; set; }
    }
}
