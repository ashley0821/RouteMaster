using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class AttractionExts
	{
		public static AttractionDetailVM ToDetailVM (this AttractionDetailDto dto)
		{
			return new AttractionDetailVM {
				Id = dto.Id,
				Category = dto.Category,
				Region = dto.Region,
				Town = dto.Town,
				Name = dto.Name,
				Address = dto.Address,
				PositionX = dto.PositionX,
				PositionY = dto.PositionY,
				Description = dto.Description,
				Website = dto.Website,
				Images = dto.Images,
				AverageScoreText = dto.AverageScoreText,
				AverageStayHoursText = dto.AverageStayHoursText,
				AveragePriceText = dto.AveragePriceText
				
			};
		}

		public static AttractionImageIndexVM ToImageIndexVM (this AttractionImageIndexDto dto)
		{
			return new AttractionImageIndexVM
			{
				Id = dto.Id,
				Image = dto.Image,
			};
		}

		public static AttractionImageIndexDto ToImageIndexDto(this AttractionImageIndexVM vm)
		{
			return new AttractionImageIndexDto
			{
				Id = vm.Id,
				Image = vm.Image,
				AttractionId = vm.AttractionId,
			};
		}

		public static AttractionCreateDto ToCreateDto(this AttractionCreateVM vm)
		{
			return new AttractionCreateDto
			{
				AttractionCategoryId = vm.AttractionCategoryId,
				RegionId = vm.RegionId,
				TownId = vm.TownId,
				Name = vm.Name,
				Address = vm.Address,
				PositionX = vm.PositionX,
				PositionY = vm.PositionY,
				Description = vm.Description,
				Website = vm.Website,
				TagId = vm.TagId,
			};
		}

		public static AttractionIndexVM ToIndexVM(this AttractionIndexDto dto)
		{
			return new AttractionIndexVM
			{
				Id = dto.Id,
				Category = dto.Category,
				Region = dto.Region,
				Town = dto.Town,
				Name = dto.Name,
				Image = dto.Image,
				DescriptionText = dto.DescriptionText,
				AverageScoreText = dto.AverageScoreText,
				AveragePriceText = dto.AveragePriceText,
				AverageStayHoursText = dto.AverageStayHoursText,
				
			};
		}


		public static AttractionEditVM ToEditVM (this AttractionEditDto dto)
		{
			return new AttractionEditVM
			{
				Id = dto.Id,
				AttractionCategoryId = dto.AttractionCategoryId,
				RegionId = dto.RegionId,
				TownId = dto.TownId,
				Name = dto.Name,
				Address = dto.Address,
				PositionX = dto.PositionX,
				PositionY = dto.PositionY,
				Description = dto.Description,
				Website = dto.Website,
			};
		}

		public static AttractionEditDto ToEditDto (this AttractionEditVM vm)
		{
			return new AttractionEditDto
			{
				Id = vm.Id,
				AttractionCategoryId = vm.AttractionCategoryId,
				RegionId = vm.RegionId,
				TownId = vm.TownId,
				Name = vm.Name,
				Address = vm.Address,
				PositionX = vm.PositionX,
				PositionY = vm.PositionY,
				Description = vm.Description,
				Website = vm.Website,
			};
		}
	}
}