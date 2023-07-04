using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
    public class PackageTourCreateVM
    {
        [Required]
        [Display(Name = "套裝行程簡介")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "上架狀態")]
        public bool Status { get; set; }

        [Required]
        [Display(Name = "折扣力度")]
        public int? CouponId { get; set; }


        [Display(Name = "活動列表")]
        public List<ActivityIndexVM> Activities { get; set; }

    
        [Display(Name = "額外服務列表")]
        public List<ExtraServiceIndexVM> ExtraServices { get; set; }

        [Display(Name = "景點列表")]
        public List<AttractionListIndexVM> Attractions { get; set; }
    }
}