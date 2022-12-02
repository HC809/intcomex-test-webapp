using System;
using System.Collections.Generic;

namespace intcomex_test_webapp.DL.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            DeptEmps = new HashSet<DeptEmp>();
            Salaries = new HashSet<Salary>();
        }

        public int EmpNo { get; set; }
        public DateOnly BirthDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateOnly HireDate { get; set; }

        public virtual ICollection<DeptEmp> DeptEmps { get; set; }
        public virtual ICollection<Salary> Salaries { get; set; }
    }
}
