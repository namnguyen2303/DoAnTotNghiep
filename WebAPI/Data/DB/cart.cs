namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class cart
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public cart()
        {
            cartDetails = new HashSet<cartDetail>();
        }

        [Key]
        public int id { get; set; }

        public DateTime? createDate { get; set; }

        public int customerId { get; set; }

        public decimal totalPrices { get; set; }

        public int totalItems { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cartDetail> cartDetails { get; set; }

        public virtual customer customer { get; set; }
    }
}
