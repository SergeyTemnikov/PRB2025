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
        public ObservableCollection<CalendarNode> Days { get; set; }
    }
}
