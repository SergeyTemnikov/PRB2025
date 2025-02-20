using prb_session5.Models;
using System.Globalization;
using Event = prb_session5.Models.Event;

namespace prb_session5.Shared
{
    partial class Calendar
    {

        protected override async Task OnInitializedAsync()
        {
            await FetchData();
            UpdateCalendar();
        }

        private DateTime CurrentDate = DateTime.Today;
        private string CurrentDateString;
        private DateTime FirstDate;
        private DateTime LastDay;
        private DateTime DayCounter;
        private string[] dayNames;

        string next = ">";
        string before = "<";

        private void PreviousMonth()
        {
            CurrentDate = CurrentDate.AddMonths(-1);
            UpdateCalendar();
        }

        private void NextMonth()
        {
            CurrentDate = CurrentDate.AddMonths(1);
            UpdateCalendar();
        }

        private void UpdateCalendar()
        {
            FirstDate = new DateTime(CurrentDate.Year, CurrentDate.Month, 1);
            LastDay = FirstDate.AddMonths(1);
            DayCounter = FirstDate;

            var str = CurrentDate.ToString("MMMM yyyy");
            CurrentDateString = char.ToUpper(str[0]) + str.Substring(1);

            dayNames = CultureInfo.CurrentCulture.DateTimeFormat.AbbreviatedDayNames;

            for (int i = 0; i < 7; i++)
            {
                dayNames[i] = dayNames[i].Remove(1).ToUpper();
            }
        }

        internal static int GetNumOfDay(int numOfDay)
        {
            if (numOfDay < 0) return 0;
            if (numOfDay > 6)
            {
                numOfDay %= 7;
            }

            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
            if (numOfDay >= 0) numOfDay++;
            if (numOfDay == 7) numOfDay = 0;


            return numOfDay;
        }

        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        private bool IsToday(DateTime date)
        {
            return date == DateTime.Today;
        }

        private HttpClient httpClient;
        private List<Event> _events;
        private List<Birthday> _birthdays;

        private async Task FetchData()
        {
            httpClient = new HttpClient();

            _events = await httpClient.GetFromJsonAsync<List<Event>>("https://localhost:7208/api/Events/GetEvents");
            _birthdays = await httpClient.GetFromJsonAsync<List<Birthday>>("https://localhost:7208/api/Birthdays/GetBirthdays");
        }

        private string IsEvent(DateTime date)
        {
            List<Event> events = _events.Where(x => Convert.ToDateTime(x.Date) == date).ToList();

            if (events == null) return "";

            var count = events.Count();

            if (count >= 5) return " event-high";

            if (count < 5 && count > 2) return " event-medium";

            if (count <= 2 && count > 0) return " event-low";

            return "";
        }

        private bool IsBirthday(DateTime date)
        {
            var birthday = _birthdays.Where(x => x.BirthDate.Month == date.Month && x.BirthDate.Day == date.Day).FirstOrDefault();

            if (birthday == default) return false;

            return true;
        }

    }
}
