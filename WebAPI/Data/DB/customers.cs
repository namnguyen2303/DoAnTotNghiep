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
    
    public partial class customers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customers()
        {
            this.cart = new HashSet<cart>();
            this.orders = new HashSet<orders>();
        }
    
        public int id { get; set; }
        public string name_customer { get; set; }
        public string phone { get; set; }
        public string pass { get; set; }
        public string address_customer { get; set; }
        public Nullable<int> gender { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string email { get; set; }
        public string avatar_url { get; set; }
        public int is_active { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> cart { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders> orders { get; set; }
    }
}
