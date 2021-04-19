namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            news = new HashSet<news>();
            orders = new HashSet<order>();
            userRoles = new HashSet<userRole>();
        }

        [Key]
        public int id { get; set; }

        [StringLength(50)]
        public string code { get; set; }

        public int? status { get; set; }
        //public int roleId { get; set; }

        [StringLength(100), Required(ErrorMessage = "Tài khoản không được bỏ trống")]
        public string username { get; set; }

        [StringLength(100), Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
        public string pass { get; set; }

        [StringLength(15), Required(ErrorMessage = "Số điện thoại không được bỏ trống"), Phone(ErrorMessage = "Không đúng định dạng")]
        public string phone { get; set; }

        public int is_active { get; set; }

        public DateTime? created_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<news> news { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order> orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<userRole> userRoles { get; set; }

    }
}
