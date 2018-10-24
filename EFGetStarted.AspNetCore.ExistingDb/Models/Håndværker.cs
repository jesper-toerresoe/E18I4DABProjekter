using System;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Models
{
    public partial class Håndværker
    {
        public Håndværker()
        {
            Værktøjskasse = new HashSet<Værktøjskasse>();
        }

        public int HåndværkerId { get; set; }
        public DateTime Ansættelsedato { get; set; }
        public string Efternavn { get; set; }
        public string Fagområde { get; set; }
        public string Fornavn { get; set; }

        public ICollection<Værktøjskasse> Værktøjskasse { get; set; }
    }
}
