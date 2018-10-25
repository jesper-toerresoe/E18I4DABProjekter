using System;
using System.Collections.Generic;

namespace EFE18Demo2.AspNetCore.ExistingDbMigration.Models
{
    public partial class Værktøjskasse
    {
        public Værktøjskasse()
        {
            Værktøj = new HashSet<Værktøj>();
        }

        public int VkasseId { get; set; }
        public DateTime Vtkanskaffet { get; set; }
        public string Vtkfabrikat { get; set; }
        public int? HåndværkerId { get; set; }
        public string Vtkmodel { get; set; }
        public string Vtkserienummer { get; set; }
        public string Vtkfarve { get; set; }

        public Håndværker Håndværker { get; set; }
        public ICollection<Værktøj> Værktøj { get; set; }
    }
}
