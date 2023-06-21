namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AttractionImage
    {
        public int Id { get; set; }

        public int AttractionId { get; set; }

        [Required]
        public string Image { get; set; }

        public virtual Attraction Attraction { get; set; }
    }
}
