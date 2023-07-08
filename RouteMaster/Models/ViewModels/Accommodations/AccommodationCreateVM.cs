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

        [Required]
        [Display(Name = "縣市")]
        public int RegionId { get; set; }

        [Required]
        [Display(Name = "鄉鎮市區")]
        public int TownId { get; set; }

        [Required]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Required]
		[RegularExpression(@"^[\d()-]{7,15}$", ErrorMessage = "號碼驗證錯誤, 請確認後再行輸入")]
		[Display(Name = "聯絡手機或市話")]
        public string PhoneNumber { get; set; }

        [Required]
		[EmailAddress(ErrorMessage = "请输入有效的电子邮件地址。")]
		[Display(Name = "聯絡Email")]
        public string IndustryEmail { get; set; }
    }
}