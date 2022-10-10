using System;
using System.Collections.Generic;

namespace CP_ComputerClub.Models
{
    public partial class FeaturesPc
    {
        public FeaturesPc()
        {
            Computers = new HashSet<Computer>();
            Histories = new HashSet<History>();
        }

        public int Id { get; set; }
        public string TypePc { get; set; } = null!;
        public string VideoCard { get; set; } = null!;
        public string Cpu { get; set; } = null!;
        public string Ram { get; set; } = null!;
        public string Monitor { get; set; } = null!;
        public string Mouse { get; set; } = null!;
        public string Keyboard { get; set; } = null!;
        public string Headphones { get; set; } = null!;
        public decimal Price { get; set; }

        public virtual ICollection<Computer> Computers { get; set; }
        public virtual ICollection<History> Histories { get; set; }
    }
}
