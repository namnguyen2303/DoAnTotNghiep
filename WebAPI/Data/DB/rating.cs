namespace Data.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("rating")]
    public partial class rating
    {
        [Key]
        public int id { get; set; }

        public int customer_id { get; set; }

        public int product_id { get; set; }

        public int? value { get; set; }

        public virtual customer customer { get; set; }

        public virtual product product { get; set; }
    }
}
