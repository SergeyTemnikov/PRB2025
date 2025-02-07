using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prb_session2_first_try.ClassHelpers
{
    public class CalendarNode
    {
        public string Name { get; set; }
        public ObservableCollection<CalendarDays> Days { get; set; }
    }

    public class CalendarDays
    {
        public string Name { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
