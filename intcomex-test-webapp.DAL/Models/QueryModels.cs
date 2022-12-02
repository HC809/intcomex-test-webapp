using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace intcomex_test_webapp.DAL.Models
{
    public record DepartmentsTotalSalaries(string name, decimal total);

    public class EmployeeTotalBorn
    {
        public int empno { get; set; }
        public DateTime birthdate { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string gender { get; set; }
        public DateTime hiredate { get; set; }
        public int same { get; set; }
        public int earlier { get; set; }
        public int later { get; set; }
    }
}
