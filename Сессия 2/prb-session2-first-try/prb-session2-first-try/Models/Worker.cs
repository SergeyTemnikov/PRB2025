using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace prb_session2_first_try.Models
{
    public partial class Worker
    {
        public int IdWorker { get; set; }

        public string FullName { get; set; }

        public int IdPosition { get; set; }

        public string WorkPhoneNumber { get; set; }

        public int? IdCabinet { get; set; }

        public string Email { get; set; }

        public int? IdLead { get; set; }

        public int? IdHelper { get; set; }

        public int IdDepartament { get; set; }

        public int? IdPrivateInfo { get; set; }

        public bool IsWorking { get; set; }

        public DateTime? LastWorkDay { get; set; }
    }
}
