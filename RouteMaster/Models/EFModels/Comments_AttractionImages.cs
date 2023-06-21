namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments_AttractionImages
    {
        public int Id { get; set; }

        public int Comments_AttractionId { get; set; }

        [Required]
        public string Image { get; set; }

        public virtual Comments_Attractions Comments_Attractions { get; set; }
    }
}
