namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExtraServicesDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ExtraServiceId { get; set; }

        [Required]
        public string ExtraServiceName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public virtual ExtraService ExtraService { get; set; }

        public virtual Order Order { get; set; }
    }
}
