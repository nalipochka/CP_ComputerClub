using System;
using System.Collections.Generic;

namespace CP_ComputerClub.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public decimal TotalSum { get; set; }
        public int TypePcid { get; set; }

        public virtual FeaturesPc TypePc { get; set; } = null!;
    }
}
