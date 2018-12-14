using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTPM_HTQLNH_SHARE.Models
{
    public class InvoiceViewModel
    {
        public Invoice header;
        public IEnumerable<InvoiceDetail> details;


    }
}
