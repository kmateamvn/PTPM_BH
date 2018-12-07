using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTPM_HTQLNH_SHARE.API
{
    public class Response
    {
        public int error { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
