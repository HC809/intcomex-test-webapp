using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intcomex_test_webapp.BLL.DTOs
{
    public class UserDTO
    {
        public int UserNo { get; set; }
        [Required] public string CodUser { get; set; } = "xmxwebdemo2";
        [Required][StringLength(6)][RegularExpression(@"^xmx")] public string Username { get; set; } = null!;
        [Required][MaxLength(15)][MinLength(5)] public string Name { get; set; } = null!;
        [Required][MaxLength(10)][MinLength(5)] public string Title { get; set; } = null!;
        [Required][StringLength(10)][RegularExpression(@"^+57")] public string Phone { get; set; } = null!;
        [Required][EmailAddress] public string Email { get; set; } = null!;
        [Required] public string ContactTypeNo { get; set; } = null!;
        public string ContactTypeName { get; set; } = null!;
        public bool IsAutorizeWebstore { get; set; }
        public bool IsAutorizeOrders { get; set; }
    }
}
