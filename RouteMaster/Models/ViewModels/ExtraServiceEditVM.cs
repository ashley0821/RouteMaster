using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class ExtraServiceEditVM
	{
  

        [Display(Name = "額外服務項目編號")]
        [Required]
        public int Id { get; set; }


        [Display(Name = "額外活動名稱")]
        [Required]
        public string Name { get; set; }


        [Display(Name = "附屬景點")]
        [Required]
        public int AttractionId { get; set; }

        public string AttractionName { get; set; }

        [Display(Name = "價格")]
        [Required]
        public int Price { get; set; }


        [Display(Name = "服務項目說明")]
        [Required]
        public string Description { get; set; }


        [Display(Name = "服務項目說明")]
        public string DescriptionText
        {
            get
            {
                return this.Description.Length > 30
                    ? this.Description.Substring(0, 30) + "...."
                    : this.Description;
            }
        }

        [Display(Name = "上架狀態")]
        [Required]
        public bool Status { get; set; }



        [Display(Name = "上架狀態")]
        public string StatusText
        {
            get
            {
                return Status == true ? "上架中" : "已下架";
            }
        }
    }
}