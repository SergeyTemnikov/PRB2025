using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prb_session2_first_try.Models
{
    public partial class WorkerPrivateInfo
    {
        public int IdInfo { get; set; }

        public int IdWorker { get; set; }

        public string PrivatePhoneNumber { get; set; }

        public DateTime? Birthday { get; set; }

    }
}
