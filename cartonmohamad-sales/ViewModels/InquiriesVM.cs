using cartonmohamad_sales.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cartonmohamad_sales.ViewModels
{
    public class InquiriesVM
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public string Q { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public System.Collections.Generic.List<InqRowVM> Items { get; set; }
            = new System.Collections.Generic.List<InqRowVM>();
    }
    public class InqRowVM
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string NumberOrderText { get; set; }
        public System.DateTime Date { get; set; }
        public string Status { get; set; }
    }
}