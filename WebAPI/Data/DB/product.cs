namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            cartDetails = new HashSet<cartDetail>();
            colors = new HashSet<color>();
            orderDetails = new HashSet<orderDetail>();
            sales = new HashSet<sale>();
            ratings = new HashSet<rating>();
        }
        [Key]
        public int id { get; set; }

        public int product_category_id { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [StringLength(255)]
        public string product_name { get; set; }

        public decimal? price_start { get; set; }

        public decimal? price_sale { get; set; }

        [StringLength(255)]
        public string summary { get; set; }

        [StringLength(255)]
        public string description { get; set; }

        [Column(TypeName = "text")]
        public string detail { get; set; }

        [StringLength(255)]
        public string slug { get; set; }

        [StringLength(5)]
        public string size { get; set; }

        public int? active { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }

        public string more_image { get; set; }

        public int? is_hot { get; set; }

        public int? is_new { get; set; }

        public int? is_sale { get; set; }

        public int is_active { get; set; }

        public DateTime? created_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cartDetail> cartDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<color> colors { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderDetail> orderDetails { get; set; }

        public virtual product_categories product_categories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<sale> sales { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rating> ratings { get; set; }
    }
}
