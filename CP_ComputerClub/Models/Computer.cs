using System;
using System.Collections.Generic;

namespace CP_ComputerClub.Models
{
    public partial class Computer
    {
        public int Id { get; set; }
        public int TypePcid { get; set; }
        public bool IsBusy { get; set; }
        public bool IsBroken { get; set; }
        public DateTime? TimeStart { get; set; }
        public DateTime? TimeEnd { get; set; }

        public virtual FeaturesPc TypePc { get; set; } = null!;
    }
}
