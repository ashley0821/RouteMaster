using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
    public class PackageTourEditDto
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Status { get; set; }

        public int? CouponId { get; set; }

        public List<ActivityEditVM> Activities { get; set; }
        public List<ExtraServiceEditVM> ExtraServices { get; set; }

        public List<AttractionListEditVM> Attractions { get; set; }
    }
}