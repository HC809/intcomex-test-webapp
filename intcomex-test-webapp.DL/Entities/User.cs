using System;
using System.Collections.Generic;

namespace intcomex_test_webapp.DL.Entities
{
    public partial class User
    {
        public int UserNo { get; set; }
        public string CodUser { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactTypeNo { get; set; }
        public bool IsAutorizeWebstore { get; set; }
        public bool IsAutorizeOrders { get; set; }

        public virtual ContactType ContactTypeNoNavigation { get; set; }
    }
}
