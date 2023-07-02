namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AccommodationImage
    {
        public int Id { get; set; }

        public int AccommodationId { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        public string Image { get; set; }

        public virtual Accommodation Accommodation { get; set; }
    }
}
