using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDbMigration.Models
{
    public partial class Haandvaerker
    {
        public Haandvaerker()
        {
            Vaerktoejskasse = new HashSet<Vaerktoejskasse>();
        }

        public int HaandvaerkerId { get; set; }
        public DateTime HVAnsaettelsedato { get; set; }
        public string HVEfternavn { get; set; }
        public string HVFagomraade { get; set; }
        public string HVFornavn { get; set; }

        public ICollection<Vaerktoejskasse> Vaerktoejskasse { get; set; }
    }
}
