using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class Værktøj
    {
        public long VærktøjsId { get; set; }
        public DateTime Anskaffet { get; set; }
        public string Fabrikat { get; set; }
        public string Model { get; set; }
        public string Serienr { get; set; }
        public string Type { get; set; }
        public int? LiggerIvtk { get; set; }

        public Værktøjskasse LiggerIvtkNavigation { get; set; }
    }
}
