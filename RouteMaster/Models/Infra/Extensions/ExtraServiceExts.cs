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
				AttractionName = dto.AttractionName,
				Price = dto.Price,			
				Description = dto.Description,
				Status = dto.Status,
			};
		}



        public static ExtraServiceIndexDto ToIndexDto(this ExtraServiceIndexVM vm)
        {
            return new ExtraServiceIndexDto
            {
                Id = vm.Id,
                Name = vm.Name,
				AttractionName = vm.AttractionName,
                Price = vm.Price,
                Description = vm.Description,
                Status = vm.Status,
            };
        }




		public static int GetAttractionIdFromId(this int id)
		{
			AppDbContext db = new AppDbContext();
			int result = db.ExtraServices.FirstOrDefault(e=>e.Id==id).AttractionId;
			return result;
		}

		public static ExtraService ToEntity(this ExtraServiceIndexDto dto)
        {
            return new ExtraService
            {
                Id = dto.Id,
                Name = dto.Name,
				AttractionId= GetAttractionIdFromId(dto.Id),
				Price = dto.Price,
                Description = dto.Description,
                Status = dto.Status,
            };
        }



        public static ExtraServiceIndexDto ToIndexDto(this ExtraService entity)
		{
			return new ExtraServiceIndexDto
			{
				Id=entity.Id,		
				Name = entity.Name,	
				AttractionName = entity.Attraction.Name,
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
				Price= vm.Price,
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
				Id = entity.Id,
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
				Id= dto.Id,
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
				Id = vm.Id,
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
				Id = dto.Id,
				AttractionId = dto.AttractionId,
				Name = dto.Name,
				Price = dto.Price,
				Description = dto.Description,
				Status = dto.Status,
			};
		}



	}
}