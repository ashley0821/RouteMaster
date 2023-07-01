using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class FAQExts
	{
		public static FAQIndexVM ToFAQIndexVM(this FAQ entity)
		{
			return new FAQIndexVM
			{
				Id = entity.Id,
				CategoryName = entity.FAQCategory.Name,
				Question = entity.Question,
				Answer = entity.Answer,
				Helpful = entity.Helpful,
				CreateDate = entity.CreateDate,
				ModifiedDate = entity.ModifiedDate
			};
		}

	}
}