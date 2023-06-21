namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transportation")]
    public partial class Transportation
    {
        public int Id { get; set; }

        public int TravelPlanId { get; set; }

        [Required]
        public string Vehicle { get; set; }

        [Required]
        public string Departure { get; set; }

        [Required]
        public string Arrival { get; set; }

        public TimeSpan TimeSpent { get; set; }

        public virtual TravelPlan TravelPlan { get; set; }
    }
}
