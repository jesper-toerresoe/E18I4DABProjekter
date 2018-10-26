using System;
using System.Collections.Generic;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Models
{
    public partial class Vaerktoej
    {
        public long VaerktoejsId { get; set; }
        public DateTime Vtanskaffet { get; set; }
        public string Vtfabrikat { get; set; }
        public string Vtmodel { get; set; }
        public string Vtserienr { get; set; }
        public string Vttype { get; set; }
        public int? Vtkid { get; set; }

        public Vaerktoejskasse Vtk { get; set; }
    }
}
