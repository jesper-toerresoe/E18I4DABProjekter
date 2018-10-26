using System;
using System.Collections.Generic;

namespace EFE18Demo4.AspNetCore.ExistingDbMigration.Models
{
    public partial class Vaerktoejskasse
    {
        public Vaerktoejskasse()
        {
            Vaerktoej = new HashSet<Vaerktoej>();
        }

        public int VkasseId { get; set; }
        public DateTime Vtkanskaffet { get; set; }
        public string Vtkfabrikat { get; set; }
        public int? HaandvaerkerId { get; set; }
        public string Vtkmodel { get; set; }
        public string Vtkserienummer { get; set; }
        public string Vtkfarve { get; set; }

        public Haandvaerker Haandvaerker { get; set; }
        public ICollection<Vaerktoej> Vaerktoej { get; set; }
    }
}
