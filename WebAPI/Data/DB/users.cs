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
    
    public partial class users
    {
        public int id { get; set; }
        public Nullable<int> role { get; set; }
        public string username { get; set; }
        public string pass { get; set; }
        public string phone { get; set; }
        public int is_active { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    }
}
