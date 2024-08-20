using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARCN.Application.DataModels.ContentManagement
{
    public class DashboardData
    {
        public double TotalNARs { get; set; }
        public double NARGrowth { get; set; }
        public double TotalNewsLetter { get; set; }
        public double NewsLetterGrowth { get; set; }
        public double TotalPrograms { get; set; }
        public double ProgramGrowth { get; set; }
        public double TotalFCAs { get; set; }
        public double FCAGrowth { get; set; }
        public double TotalJournal { get; set; }
        public double JournalGrowth { get; set; }
        public double TotalEvents { get; set; }
        public double EventGrowth { get; set; }
    }
}
