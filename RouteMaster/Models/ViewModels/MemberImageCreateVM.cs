using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels
{
	public class MemberImageCreateVM
	{
		public int Id { get; set; }

		public int MemberId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public string Image { get; set; }

	}
}