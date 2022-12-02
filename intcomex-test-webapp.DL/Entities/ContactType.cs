using System;
using System.Collections.Generic;

namespace intcomex_test_webapp.DL.Entities
{
    public partial class ContactType
    {
        public ContactType()
        {
            Users = new HashSet<User>();
        }

        public string ContactTypeNo { get; set; }
        public string ContactTypeName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
