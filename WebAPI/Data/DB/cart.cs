//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class cart
    {
        public int id { get; set; }
        public Nullable<int> status { get; set; }
        public Nullable<double> total_price { get; set; }
        public int customer_id { get; set; }
        public Nullable<System.DateTime> date_buy { get; set; }
        public string note { get; set; }
        public int is_active { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    
        public virtual customers customers { get; set; }
    }
}