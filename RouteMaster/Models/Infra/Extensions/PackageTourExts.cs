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




  
	}

}
