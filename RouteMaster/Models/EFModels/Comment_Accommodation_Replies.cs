namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment_Accommodation_Replies
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Comments_AccommodationId { get; set; }

        [Key]
        [Column(Order = 2)]
        public string Content { get; set; }

        public virtual Comments_Accommodations Comments_Accommodations { get; set; }
    }
}
