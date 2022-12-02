using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace intcomex_test_webapp.BLL.Models
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; } = false;
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
    }
}
