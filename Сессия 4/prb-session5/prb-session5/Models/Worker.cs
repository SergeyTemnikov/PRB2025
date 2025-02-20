namespace prb_session5.Models
{
    public class Worker
    {
        public int IdWorker { get; set; }

        public string FullName { get; set; } = null!;

        public int IdPosition { get; set; }

        public string WorkPhoneNumber { get; set; } = null!;

        public int? IdCabinet { get; set; }

        public string Email { get; set; } = null!;

        public int? IdLead { get; set; }

        public int? IdHelper { get; set; }

        public int IdDepartament { get; set; }

        public bool IsWorking { get; set; }

        public DateTime? LastWorkDay { get; set; }
    }
}
