namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart_AccommodationDetails
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int AccommodationId { get; set; }

        public virtual Accommodation Accommodation { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
