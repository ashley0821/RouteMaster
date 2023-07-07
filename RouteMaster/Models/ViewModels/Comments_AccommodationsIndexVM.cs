using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.ViewModels
{
	public class Comments_AccommodationsIndexVM
	{
        public int Id { get; set; }

		[Display(Name = "帳號名稱")]
		public string Account { get; set; }

		[Display(Name = "住宿名稱")]
		public string Name { get; set; }

		[Display(Name = "評分")]
		public double Score { get; set; }

		[Display(Name = "評論標題")]
		public string Title { get; set; }

		[Display(Name = "評論建立時間")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm}")]
		public DateTime? CreateDate { get; set; }   
		

    }
}