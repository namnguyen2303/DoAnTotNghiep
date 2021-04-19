namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public order()
        {
            orderDetails = new HashSet<orderDetail>();
        }
        [Key]
        public int id { get; set; }

        public int customer_id { get; set; }
        [ForeignKey("user")]
        public int? userId { get; set; }

        public int? status { get; set; }

        public decimal total_price { get; set; }

        public DateTime? date_buy { get; set; }

        [StringLength(255)]
        public string note { get; set; }

        public DateTime? created_at { get; set; }

        public int totalItem { get; set; }
        [ForeignKey("Payment")]
        public int paymentId { get; set; }

        public virtual customer customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderDetail> orderDetails { get; set; }
        public virtual payment Payment { get; set; }
        public virtual user user { get; set; }
    }
}
