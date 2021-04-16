namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class slide
    {
        [Key]
        public int id { get; set; }

        public string summary { get; set; }

        [StringLength(255)]
        public string image_url { get; set; }

        [StringLength(255)]
        public string slug { get; set; }

        public int? type { get; set; }

        public int? status { get; set; }

        public int is_active { get; set; }

        public DateTime? created_at { get; set; }
    }
}
