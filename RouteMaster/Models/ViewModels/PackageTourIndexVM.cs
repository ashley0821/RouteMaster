using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
    public class PackageTourIndexVM
    {
        public int Id { get; set; }

       
        [Display(Name = "套裝行程簡介")]
        public string Description { get; set; }

      
        [Display(Name = "上架狀態")]
        public bool Status { get; set; }

      
        [Display(Name = "優惠券")]
        public int? CouponId { get; set; }

       
        [Display(Name = "活動列表")]

        public List<ActivityIndexVM> Activities { get; set; }

      
        [Display(Name = "額外服務列表")]
        public List<ExtraServiceIndexVM> ExtraServices { get; set; }


     
        [Display(Name = "景點列表")]
        public List<AttractionListIndexVM> Attractions { get; set; }
    }
}