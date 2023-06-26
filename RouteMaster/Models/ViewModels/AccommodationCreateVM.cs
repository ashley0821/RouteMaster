using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
    public class AccommodationCreateVM
    {

        public int Id { get; set; }

        public int PartnerId { get; set; }

        [Required]
        [Display(Name = "貴住宿的名稱")]
        [StringLength(850)]
        public string Name { get; set; }

        [Display(Name = "所在縣市")]
        public int RegionId { get; set; }

        [Display(Name = "鄉鎮市區")]
        public int TownId { get; set; }

        [Required]
        [Display(Name = "貴住宿的地址")]
        public string Address { get; set; }

        [Display(Name = "聯絡手機或市話")]
        public string PhoneNumber { get; set; }

        [Display(Name = "聯絡Email")]
        public string IndustryEmail { get; set; }
    }
}