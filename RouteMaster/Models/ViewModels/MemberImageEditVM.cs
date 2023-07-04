using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class MemberImageEditVM
	{
		public int Id { get; set; }

		[StringLength(70)]
		public string Image { get; set; }
	}
}