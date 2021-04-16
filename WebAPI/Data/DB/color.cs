namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class color
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        public int? code { get; set; }

        [StringLength(100)]
        public string color_name { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(255)]
        public string image_url { get; set; }

        public int product_id { get; set; }

        public int? is_active { get; set; }

        public DateTime? created_at { get; set; }

        public virtual product product { get; set; }
    }
}
