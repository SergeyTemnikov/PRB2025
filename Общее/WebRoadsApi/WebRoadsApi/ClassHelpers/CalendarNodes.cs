using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace WebRoadsApi.ClassHelpers
{
    public class CalendarNode
    {
        public string Name { get; set; }
        public ObservableCollection<CalendarNode> Days { get; set; }
    }
}
