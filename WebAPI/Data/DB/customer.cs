namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public customer()
        {
            carts = new HashSet<cart>();
            orders = new HashSet<order>();
            ratings = new HashSet<rating>();
            userRoles = new HashSet<userRole>();
        }
        [Key]

        public int id { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        [StringLength(50)]
        public string name_customer { get; set; }

        [StringLength(15)]
        public string phone { get; set; }

        [StringLength(250), Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string pass { get; set; }

        [StringLength(255)]
        public string address_customer { get; set; }

        public int? gender { get; set; }

        public int? type { get; set; }

        public int? status { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(100)]
        public string email { get; set; }

        [StringLength(255)]
        public string avatar_url { get; set; }

        public int is_active { get; set; }

        public DateTime? created_at { get; set; }

        [ForeignKey("CustomerType")]
        public int CustomerTypeId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rating> ratings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userRole> userRoles { get; set; }

        public virtual customerType CustomerType { get; set; }
    }
}
