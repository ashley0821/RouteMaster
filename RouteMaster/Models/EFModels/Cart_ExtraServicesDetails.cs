namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart_ExtraServicesDetails
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ExtraServiceId { get; set; }

        public int Quantity { get; set; }

        public virtual ExtraService ExtraService { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
