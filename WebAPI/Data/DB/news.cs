namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class news
    {
        [Key]
        public int id { get; set; }

        public int user_id { get; set; }

        public string summary { get; set; }

        [Column(TypeName = "text")]
        public string content { get; set; }

        [StringLength(255)]
        public string imageUrl { get; set; }

        public int? is_hot { get; set; }

        public int? is_new { get; set; }

        public int? type_new { get; set; }

        public int? status { get; set; }

        public int is_active { get; set; }

        public DateTime? created_at { get; set; }

        public virtual user user { get; set; }
    }
}
