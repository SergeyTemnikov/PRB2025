using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prb_session2_first_try.Models
{
    public class Departament
    {
        public int IdDepartament { get; set; }

        public string NameDepartament { get; set; } = null!;

        public int? IdParentDepartament { get; set; }

        public string? DepartamentDescription { get; set; }

        public int? IdHeadOfDepartament { get; set; }

    }
}
