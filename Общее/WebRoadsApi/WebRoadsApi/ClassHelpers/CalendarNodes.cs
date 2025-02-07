using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace WebRoadsApi.ClassHelpers
{
    public class CalendarNode
    {
        public string Name { get; set; }
        public ObservableCollection<CalendarDays> Days { get; set; }
    }

    public class CalendarDays
    {
        public string Name { get {
                if(DateStart == DateEnd)
                {
                    return DateStart.ToString("d");
                }
                else
                {
                    return DateStart.ToString("d") + " - " + DateEnd.ToString("d");
                }
            } }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
    }
}
