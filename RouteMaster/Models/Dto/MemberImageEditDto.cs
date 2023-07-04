using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class MemberImageEditDto
	{
		public int Id { get; set; }

		public string Image { get; set; }
	}

	public static class MemberImageExts
	{
		//把MemberVM的東西轉成MemberImageVM
		public static MemberImageEditVM ToMemberImageVM(this Member dto) 
		{
			return new MemberImageEditVM()
			{
				Id = dto.Id,
				Image = dto.Image,
			};
		}
	}
}