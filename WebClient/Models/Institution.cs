namespace WebClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("robinfoo_lgp.Institution")]
    public partial class Institution
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? group_id { get; set; }

        [Required]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [StringLength(255)]
        public string address { get; set; }

        [Required]
        [StringLength(50)]
        public string city { get; set; }

        public decimal latitude { get; set; }

        public decimal longitude { get; set; }

        [Required]
        [StringLength(255)]
        public string email { get; set; }

        [Required]
        [StringLength(255)]
        public string website { get; set; }

        [Required]
        [StringLength(50)]
        public string phone_number { get; set; }

        [StringLength(50)]
        public string fax { get; set; }
    }
}
