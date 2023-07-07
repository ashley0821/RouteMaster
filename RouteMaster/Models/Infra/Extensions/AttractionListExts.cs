using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
    public static class AttractionListExts
    {

        public static AttractionListIndexDto ToAttractionListIndexDto(this Attraction entity)
        {
            AppDbContext _db = new AppDbContext();


            return new AttractionListIndexDto
            {
                Id = entity.Id,
                CategoryName = entity.AttractionCategory.Name,
                Region = entity.Region.Name,
                Town = entity.Town.Name,
                Name = entity.Name,
                Address = entity.Address,
                Description = entity.Description,
                AverageScore = _db.Comments_Attractions
                .Where(c => c.AttractionId == entity.Id)
                .Select(c => c.Score)
                .DefaultIfEmpty()
                .Average(),




                AverageStayHours = _db.Comments_Attractions
                .Where(c => c.AttractionId == entity.Id)
                .Select(c => c.StayHours)
                .DefaultIfEmpty()
                .Average(),

                AveragePrice = (int)Math.Round(_db.Comments_Attractions
                .Where(c => c.AttractionId == entity.Id)
                .Select(c => c.Price)
                .DefaultIfEmpty()
                .Average() ?? 0),
            };
        }


        public static AttractionListIndexVM ToAttractionListIndexVM(this AttractionListIndexDto dto)
        {
            return new AttractionListIndexVM
            {
                Id = dto.Id,
                CategoryName = dto.CategoryName,
                Region = dto.Region,
                Town = dto.Town,
                Name = dto.Name,
                Description = dto.Description,
                Address = dto.Address,
                AveragePrice = dto.AveragePrice,
                AverageStayHours = dto.AverageStayHours,
                AverageScore = dto.AverageScore,

            };
        }


        public static AttractionListIndexDto ToAttractionListIndexDto(this AttractionListIndexVM vm)
        {
            return new AttractionListIndexDto
            {
                Id = vm.Id,
                CategoryName = vm.CategoryName,
                Address = vm.Address,
                Region = vm.Region,
                Town = vm.Town,
                Name = vm.Name,
                Description = vm.Description,
            };
        }


        public static Attraction ToAttractionListIndexEntity(this AttractionListIndexDto dto)
        {
            return new Attraction
            {
                Id = dto.Id,
                AttractionCategoryId = GetAttractionCategoryIdById(dto.Id),
                RegionId = GetAttractionRegionIdById(dto.Id),
                TownId = GetAttractionTownIdById(dto.Id),
                Name = dto.Name,
                Address = dto.Address,
                Description = dto.Description,
            };
        }




		public static AttractionListEditDto ToAttractionListEditDto(this Attraction entity)
		{
			AppDbContext _db = new AppDbContext();


			return new AttractionListEditDto
			{
				Id = entity.Id,
				CategoryName = entity.AttractionCategory.Name,
				Region = entity.Region.Name,
				Town = entity.Town.Name,
				Name = entity.Name,
				Description = entity.Description,
				AverageScore = _db.Comments_Attractions
				.Where(c => c.AttractionId == entity.Id)
				.Select(c => c.Score)
				.DefaultIfEmpty()
				.Average(),


				AverageStayHours = _db.Comments_Attractions
				.Where(c => c.AttractionId == entity.Id)
				.Select(c => c.StayHours)
				.DefaultIfEmpty()
				.Average(),

				AveragePrice = (int)Math.Round(_db.Comments_Attractions
				.Where(c => c.AttractionId == entity.Id)
				.Select(c => c.Price)
				.DefaultIfEmpty()
				.Average() ?? 0),
			};
		}


		public static AttractionListEditVM ToAttractionListEditVM(this AttractionListEditDto dto)
		{
			return new AttractionListEditVM
			{
				Id = dto.Id,
				CategoryName = dto.CategoryName,
				Region = dto.Region,
				Town = dto.Town,
				Name = dto.Name,
				Description = dto.Description,
				Address = dto.Address,
				AveragePrice = dto.AveragePrice,
				AverageStayHours = dto.AverageStayHours,
				AverageScore = dto.AverageScore,

			};
		}




		public static AttractionListEditDto ToAttractionListEditDto(this AttractionListEditVM vm)
		{
			return new AttractionListEditDto
			{
				Id = vm.Id,
				CategoryName = vm.CategoryName,
				Address = vm.Address,
				Region = vm.Region,
				Town = vm.Town,
				Name = vm.Name,
				Description = vm.Description,
			};
		}



		public static Attraction ToAttractionListEditEntity(this AttractionListEditDto dto)
		{
			return new Attraction
			{
				Id = dto.Id,
				AttractionCategoryId = GetAttractionCategoryIdById(dto.Id),
				RegionId = GetAttractionRegionIdById(dto.Id),
				TownId = GetAttractionTownIdById(dto.Id),
				Name = dto.Name,
				Address = dto.Address,
				Description = dto.Description,
			};
		}




		public static int GetAttractionCategoryIdById(int id)
        {
            AppDbContext db = new AppDbContext();
            var result = db.Attractions.Find(id).AttractionCategoryId;
            return result;
        }

        public static int GetAttractionRegionIdById(int id)
        {
            AppDbContext db = new AppDbContext();
            var result = db.Attractions.Find(id).RegionId;
            return result;
        }

        public static int GetAttractionTownIdById(int id)
        {
            AppDbContext db = new AppDbContext();
            var result = db.Attractions.Find(id).TownId;
            return result;
        }

    }
}