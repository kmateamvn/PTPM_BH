//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PTPM_HTQLNH_SHARE.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.InvoiceDetails = new HashSet<InvoiceDetail>();
        }
    
        public int id { get; set; }
        public string res_id { get; set; }
        public string buyer_name { get; set; }
        public string buyer_address { get; set; }
        public string buyer_tax { get; set; }
        public string buyer_phone { get; set; }
        public string buyer_email { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<int> payment_type { get; set; }
        public Nullable<int> currency_type { get; set; }
        public Nullable<System.DateTime> transaction_date { get; set; }
        public Nullable<System.DateTime> date_create { get; set; }
        public Nullable<int> user_create { get; set; }
        public Nullable<decimal> vat { get; set; }
        public Nullable<decimal> totalAmount { get; set; }
        public Nullable<decimal> totalAmountWithVat { get; set; }
        public Nullable<decimal> discountAmount { get; set; }
        public string totalAmountWithVatWords { get; set; }
        public string note { get; set; }
        public string invoice_no { get; set; }
        public string invoice_type { get; set; }
        public string template_code { get; set; }
        public Nullable<decimal> exc_rate { get; set; }
        public Nullable<decimal> totalNaturalAmount { get; set; }
        public string invoice_seri { get; set; }
        public string invoice_pattern { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public virtual Restaurant Restaurant { get; set; }
        public virtual User User { get; set; }
    }
}
