using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class Extension
	{
		public static AccommodationIndexVM ToVM(this AccommodationIndexDto dto)
		{
			return new AccommodationIndexVM
			{
				Id = dto.Id,
				PartnerId = dto.PartnerId,
				Name = dto.Name,
				Address = dto.Address,
				AccommodationImage = dto.AccommodationImage
			};
		}
	}
}