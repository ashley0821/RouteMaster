namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SpecificRoomPrice
    {
        public int Id { get; set; }

        public int RoomId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public decimal NewPrice { get; set; }

        public virtual Room Room { get; set; }
    }
}
