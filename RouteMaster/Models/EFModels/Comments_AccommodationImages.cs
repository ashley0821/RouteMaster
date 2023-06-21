namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments_AccommodationImages
    {
        public int Id { get; set; }

        public int Comments_AccommodationId { get; set; }

        [Required]
        public string Image { get; set; }

        public virtual Comments_Accommodations Comments_Accommodations { get; set; }
    }
}
