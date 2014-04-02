namespace WebClient.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("robinfoo_lgp.Cupon")]
    public partial class Cupon
    {
        public int id { get; set; }

        public int client_id { get; set; }

        public int ad_id { get; set; }

        public int state { get; set; }

        public DateTime start_time { get; set; }

        public DateTime end_time { get; set; }

        public DateTime purchase_time { get; set; }
    }
}
