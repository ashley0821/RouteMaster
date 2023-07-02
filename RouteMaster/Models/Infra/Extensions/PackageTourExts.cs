using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
    public static class PackageTourExts
    {

        //Attractions待補

        public static PackageTourIndexVM ToIndexVM(this PackageTourIndexDto dto)
        {
            return new PackageTourIndexVM
            {
                Id = dto.Id,
                Description = dto.Description,
                Status = dto.Status,
                CouponId = dto.CouponId,
                Activities = dto.Activities,
                ExtraServices = dto.ExtraServices,

            };
        }



        public static PackageTourIndexDto ToIndexDto(this PackageTour entity)
        {
            return new PackageTourIndexDto
            {
                Id = entity.Id,
                Description = entity.Description,
                Status = entity.Status,
                CouponId = entity.CouponId,
                Activities = entity.Activities.Select(x => x.ToIndexDto().ToIndexVM()).ToList(),
                ExtraServices = entity.ExtraServices.Select(x => x.ToIndexDto().ToIndexVM()).ToList()
            };
        }



        public static PackageTourCreateDto ToCreateDto(this PackageTourCreateVM vm)
        {
            return new PackageTourCreateDto
            {
                Description = vm.Description,
                Status = vm.Status,
                CouponId = vm.CouponId,
                Activities = vm.Activities,
                ExtraServices = vm.ExtraServices,
                Attractions = vm.Attractions,
            };
        }

        public static PackageTour ToEntity(this PackageTourCreateDto dto)
        {
            return new PackageTour
            {
                Description = dto.Description,
                Status = dto.Status,
                CouponId = dto.CouponId,
                Activities = dto.Activities.Select(x => x.ToIndexDto().ToEntity()).ToList(),
                ExtraServices = dto.ExtraServices.Select(x => x.ToIndexDto().ToEntity()).ToList(),
				Attractions=dto.Attractions.Select(x=>x.ToAttractionListDto().ToAttractionListEntity()).ToList()
			};
        }







        public static PackageTourEditDto ToEditDto(this PackageTour entity)
        {
            return new PackageTourEditDto
            {
                Id = entity.Id,
                Description = entity.Description,
                Status = entity.Status,
                CouponId = entity.CouponId,
            };
        }

        public static PackageTourEditVM ToEditVM(this PackageTourEditDto dto)
        {
            return new PackageTourEditVM
            {

                Description = dto.Description,
                Status = dto.Status,
                CouponId = dto.CouponId,
            };
        }

        public static PackageTourEditDto ToEditDto(this PackageTourEditVM vm)
        {
            return new PackageTourEditDto
            {

                Description = vm.Description,
                Status = vm.Status,
                CouponId = vm.CouponId,
            };
        }

        public static PackageTour ToEntity(this PackageTourEditDto dto)
        {
            return new PackageTour
            {
                Id = dto.Id,
                Description = dto.Description,
                Status = dto.Status,
                CouponId = dto.CouponId,
            };
        }




        public static AttractionIndexDto ToAttractionListDto(this Attraction entity)
        {
            AppDbContext _db = new AppDbContext();


            return new AttractionIndexDto
            {
                Id = entity.Id,
                Category = entity.AttractionCategory.Name,
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

                AveragePrice =(int)Math.Round(_db.Comments_Attractions
                .Where(c => c.AttractionId == entity.Id)
                .Select(c => c.Price)
				.DefaultIfEmpty()
				.Average()??0),
            };
        }


        public static AttractionIndexVM ToAttractionListVM(this AttractionIndexDto dto)
        {
            return new AttractionIndexVM
            {
                Id= dto.Id,
                Category=dto.Category,
                Region= dto.Region,
                Town= dto.Town,
                Name = dto.Name,
                DescriptionText = dto.DescriptionText,
                AverageScoreText = dto.AverageScoreText,
                AverageStayHoursText = dto.AverageStayHoursText,
                AveragePriceText = dto.AveragePriceText,
            };
        }


		public static AttractionIndexDto ToAttractionListDto(this AttractionIndexVM vm)
		{
			return new AttractionIndexDto
			{
				Id = vm.Id,
				Category = vm.Category,
				Region = vm.Region,
				Town = vm.Town,
				Name = vm.Name,				
			};
		}


		public static Attraction ToAttractionListEntity(this AttractionIndexDto dto)
		{
			return new Attraction
			{
				Id = dto.Id,
				AttractionCategoryId = GetAttractionCategoryIdById(dto.Id),
				RegionId=GetAttractionRegionIdById(dto.Id), 
				TownId = GetAttractionTownIdById(dto.Id),
				Name = dto.Name,
			};
		}



        public static int GetAttractionCategoryIdById(int id)
        {
            AppDbContext db=new AppDbContext();
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
