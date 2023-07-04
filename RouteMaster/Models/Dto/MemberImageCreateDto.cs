using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class MemberImageCreateDto
	{
		public int Id { get; set; }

		public int MemberId { get; set; }
	
		public string Name { get; set; }

		public string Image { get; set; }

	}
	 public static class MemberImgaeCreateExts
	{
		public static MemberImageCreateDto ToDto(this MemberImageCreateVM vm)
		{
			return new MemberImageCreateDto()
			{
				Id = vm.Id,
				MemberId = vm.MemberId,
				Name = vm.Name,
				Image = vm.Image,
			};
		}

	}

}