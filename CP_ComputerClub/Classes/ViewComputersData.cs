using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_ComputerClub.Classes
{
    public class ViewComputerData
    {
        public int Id { get; set; }
        public string TypePC { get; set; } = null!;


        public bool Busy { get; set; }
        private string isBusy = null!;
        public string IsBusy
        {
            get { return isBusy; }
            set
            {
                if (Busy)
                    isBusy = "Busy";
                else
                    isBusy = "Free";

            }
        }


        public bool Broken { get; set; }
        private string isBroken = null!;
        public string IsBroken
        {
            get { return isBroken; }
            set
            {
                if (Broken)
                    isBroken = "Repair";
                else
                    isBroken = "Ready";

            }
        }


        public DateTime? DateTimeStart { get; set; }
        public DateTime? DateTimeEnd { get; set; }
        public string TimeStart 
        { 
            get 
            { 
                if(DateTimeStart != null)
                return DateTimeStart!.Value.ToString("dd.MM.yyyy HH:mm"); 
                else
                    return null!;
            } 
        }
        public string TimeEnd 
        { 
            get 
            {
                if (DateTimeEnd != null)
                    return DateTimeEnd!.Value.ToString("dd.MM.yyyy HH:mm");
                else
                    return null!;
            } 
        }

    }
}
