namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ActivitiesDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ActivityId { get; set; }

        [Required]
        [StringLength(100)]
        public string ActivityName { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public virtual Activity Activity { get; set; }

        public virtual Order Order { get; set; }
    }
}
