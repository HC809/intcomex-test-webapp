using System;
using System.Collections.Generic;

namespace intcomex_test_webapp.DL.Entities
{
    public partial class Department
    {
        public Department()
        {
            DeptEmps = new HashSet<DeptEmp>();
        }

        public string DeptNo { get; set; } = null!;
        public string DeptName { get; set; } = null!;

        public virtual ICollection<DeptEmp> DeptEmps { get; set; }
    }
}
