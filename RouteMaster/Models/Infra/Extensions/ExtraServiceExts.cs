using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class ExtraServiceExts
	{
		public static ExtraServiceIndexVM ToIndexVM(this ExtraServiceIndexDto dto)
		{
			return new ExtraServiceIndexVM
			{
				Id = dto.Id,
				Name = dto.Name,			
				Price = dto.Price,			
				Description = dto.Description,
				Status = dto.Status,
			};
		}



		public static ExtraServiceIndexDto ToIndexDto(this ExtraService entity)
		{
			return new ExtraServiceIndexDto
			{
		
				Name = entity.Name,	
				Price = entity.Price,	
				Description = entity.Description,
				Status = entity.Status,
			};
		}


		public static ExtraServiceCreateDto ToCreateDto(this ExtraServiceCreateVM vm)
		{
			return new ExtraServiceCreateDto
			{			
				AttractionId = vm.AttractionId,
				Name = vm.Name,
				Description = vm.Description,
				Status = vm.Status,
			};
		}

		public static ExtraService ToEntity(this ExtraServiceCreateDto dto)
		{
			return new ExtraService
			{
				
				AttractionId = dto.AttractionId,
				Name = dto.Name,		
				Price = dto.Price,		
				Description = dto.Description,
				Status = dto.Status,
			};
		}

		public static ExtraServiceEditDto ToEditDto(this ExtraService entity)
		{
			return new ExtraServiceEditDto
			{
	
				AttractionId = entity.AttractionId,
				Name = entity.Name,		
				Price = entity.Price,	
				Description = entity.Description,
				Status = entity.Status,
			};
		}

		public static ExtraServiceEditVM ToEditVM(this ExtraServiceEditDto dto)
		{
			return new ExtraServiceEditVM
			{
	
				AttractionId = dto.AttractionId,
				Name = dto.Name,
				Price = dto.Price,
				Description = dto.Description,
				Status = dto.Status,
			};
		}

		public static ExtraServiceEditDto ToEditDto(this ExtraServiceEditVM vm)
		{
			return new ExtraServiceEditDto
			{	
				AttractionId = vm.AttractionId,
				Name = vm.Name,
				Price = vm.Price,
				Description = vm.Description,
				Status = vm.Status,
			};
		}

		public static ExtraService ToEntity(this ExtraServiceEditDto dto)
		{
			return new ExtraService
			{	
				AttractionId = dto.AttractionId,
				Name = dto.Name,
				Price = dto.Price,
				Description = dto.Description,
				Status = dto.Status,
			};
		}



	}
}