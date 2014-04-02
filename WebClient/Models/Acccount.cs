namespace WebClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("robinfoo_lgp.Acccount")]
    public partial class Acccount
    {
        public int id { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string type { get; set; }

        public long fb_id { get; set; }

        public bool? receive_notification { get; set; }

        public bool? show_ads { get; set; }

        [Column(TypeName = "enum")]
        [StringLength(65532)]
        public string currency { get; set; }
    }
}
