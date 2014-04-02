namespace WebClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("robinfoo_lgp.Ad")]
    public partial class Ad
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int institution_id { get; set; }

        [Required]
        [StringLength(255)]
        public string service { get; set; }

        [Required]
        [StringLength(255)]
        public string specialty { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string description { get; set; }

        public decimal price { get; set; }

        public float? discount { get; set; }

        public DateTime start_time { get; set; }

        public DateTime end_time { get; set; }

        public int remaining_cupons { get; set; }

        public int buyed_cupons { get; set; }
    }
}
