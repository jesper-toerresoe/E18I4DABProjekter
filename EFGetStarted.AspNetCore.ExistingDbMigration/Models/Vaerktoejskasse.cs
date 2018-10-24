using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDbMigration.Models
{
    public partial class Vaerktoejskasse
    {
        public Vaerktoejskasse()
        {
            Vaerktoej = new HashSet<Vaerktoej>();
        }

        public int VTKId { get; set; }
        public DateTime VTKAnskaffet { get; set; }
        public string VTKFabrikat { get; set; }
        public int? VTKEjesAf { get; set; }
        public string VTKModel { get; set; }
        public string VTKSerienummer { get; set; }
        public string VTKFarve { get; set; }

        public Haandvaerker EjesAfNavigation { get; set; }
        public ICollection<Vaerktoej> Vaerktoej { get; set; }
    }
}
