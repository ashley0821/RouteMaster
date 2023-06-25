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


		public static ActivityCreateDto	ToCreateDto(this ActivityCreateVM vm)
		{
			return new ActivityCreateDto

			{
				ActivityCategoryId = vm.ActivityCategoryId,
				AttractionId = vm.AttractionId,
				Name = vm.Name,
				RegionId = vm.RegionId,
				Price = vm.Price,	
				StartTime = vm.StartTime,
				EndTime = vm.EndTime,
				Description = vm.Description,
				Status = vm.Status,
			};
		}

		public static Activity ToEntity(this ActivityCreateDto dto)
		{
			return new Activity
			{
				ActivityCategoryId=dto.ActivityCategoryId,
				AttractionId=dto.AttractionId,
				Name = dto.Name,
				RegionId = dto.RegionId,
				Price = dto.Price,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Description = dto.Description,
				Status = dto.Status,
			};
		}

		public static ActivityEditDto ToEditDto (this Activity entity)
		{
			return new ActivityEditDto
			{
				Id = entity.Id,
				ActivityCategoryId = entity.ActivityCategoryId,
				AttractionId = entity.AttractionId,
				Name = entity.Name,
				RegionId = entity.RegionId,
				Price = entity.Price,
				StartTime = entity.StartTime,
				EndTime = entity.EndTime,
				Description = entity.Description,
				Status = entity.Status,
			};
		}

		public static ActivityEditVM ToEditVM (this ActivityEditDto dto)
		{
			return new ActivityEditVM
			{
				Id = dto.Id,
				ActivityCategoryId = dto.ActivityCategoryId,
				AttractionId = dto.AttractionId,
				Name = dto.Name,
				RegionId = dto.RegionId,
				Price = dto.Price,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Description = dto.Description,
				Status = dto.Status,
			};
		}	

		public static ActivityEditDto ToEditDto(this ActivityEditVM vm)
		{
			return new ActivityEditDto
			{
				Id = vm.Id,
				ActivityCategoryId = vm.ActivityCategoryId,
				AttractionId = vm.AttractionId,
				Name = vm.Name,
				RegionId = vm.RegionId,
				Price = vm.Price,
				StartTime = vm.StartTime,
				EndTime = vm.EndTime,
				Description = vm.Description,
				Status = vm.Status,
			};
		}

		public static Activity ToEntity(this ActivityEditDto dto)
		{
			return new Activity
			{
				Id = dto.Id,
				ActivityCategoryId = dto.ActivityCategoryId,
				AttractionId = dto.AttractionId,
				Name = dto.Name,
				RegionId = dto.RegionId,
				Price = dto.Price,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Description = dto.Description,
				Status = dto.Status,
			};
		}


	}

	
}