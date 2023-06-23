using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.EFRepositories;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RouteMaster.Models.Infra.Extensions
{
	public static class ActivittyExts
	{
		public static ActivityIndexVM ToIndexVM(this ActivityIndexDto dto)
		{
			return new ActivityIndexVM
			{
				Id = dto.Id,
				ActivityCategoryName = dto.ActivityCategoryName,
				AttractionName = dto.AttractionName,
				Name = dto.Name,
				RegionName = dto.RegionName,
				Price = dto.Price,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Description = dto.Description,
				Status = dto.Status,

			};
		}



		public static ActivityIndexDto ToIndexDto(this Activity entity)
		{
			return new ActivityIndexDto
			{
				Id = entity.Id,
				ActivityCategoryName = entity.ActivityCategory.Name,
				AttractionName = entity.Attraction.Name,
				Name = entity.Name,
				RegionName = entity.Region.Name,
				Price = entity.Price,
				StartTime = entity.StartTime,
				EndTime = entity.EndTime,
				Description = entity.Description,
				Status = entity.Status,
			};
		}






	}

	
}