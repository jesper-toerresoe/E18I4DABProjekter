using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Migration.Models
{
    public partial class Værktøjskasse
    {
        public Værktøjskasse()
        {
            Værktøj = new HashSet<Værktøj>();
        }

        public int VkasseId { get; set; }
        public DateTime Anskaffet { get; set; }
        public string Fabrikat { get; set; }
        public int? EjesAf { get; set; }
        public string Model { get; set; }
        public string Serienummer { get; set; }
        public string Farve { get; set; }

        public Håndværker EjesAfNavigation { get; set; }
        public ICollection<Værktøj> Værktøj { get; set; }
    }
}
