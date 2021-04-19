namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class sale
    {
        [Key]
        public int id { get; set; }

        public int? product_id { get; set; }

        public double? price_sale { get; set; }

        public DateTime? date_sale { get; set; }

        public int? amount_sale { get; set; }

        public int? is_active { get; set; }

        public DateTime? created_at { get; set; }

        public virtual product product { get; set; }
    }
}
