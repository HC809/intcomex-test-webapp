using System;
using System.Collections.Generic;

namespace intcomex_test_webapp.DL.Entities
{
    public partial class DeptEmp
    {
        public int EmpNo { get; set; }
        public string DeptNo { get; set; }
        public DateOnly FromDate { get; set; }
        public DateOnly ToDate { get; set; }

        public virtual Department DeptNoNavigation { get; set; }
        public virtual Employee EmpNoNavigation { get; set; }
    }
}
