using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
    public class PackageTourIndexDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int? CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }
    }
}