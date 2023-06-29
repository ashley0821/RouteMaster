using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
    public static class TravelPlanExts
    {

        public static TravelPlanIndexVM ToIndexVM(this TravelPlanIndexDto dto)
        {
            return new TravelPlanIndexVM
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                TravelDays = dto.TravelDays,
                CreateDate = dto.CreateDate,
       

            };
        }



        public static TravelPlanIndexDto ToIndexDto(this TravelPlan entity)
        {
            return new TravelPlanIndexDto
            {
                Id = entity.Id,              
                MemberId = entity.MemberId,
                TravelDays = entity.TravelDays,
                CreateDate = entity.CreateDate,
            };
        }


        public static TravelPlanCreateDto ToCreateDto(this TravelPlanCreateVM vm)
        {
            return new TravelPlanCreateDto

            {

                MemberId = vm.MemberId,
                TravelDays = vm.TravelDays,
                CreateDate = vm.CreateDate,

            };
        }

        public static TravelPlan ToEntity(this TravelPlanCreateDto dto)
        {
            return new TravelPlan
            {

                MemberId = dto.MemberId,
                TravelDays = dto.TravelDays,
                CreateDate = dto.CreateDate,
            };
        }

        public static TravelPlanEditDto ToEditDto(this TravelPlan entity)
        {
            return new TravelPlanEditDto
            {
                Id = entity.Id,
                MemberId = entity.MemberId,
                TravelDays = entity.TravelDays,
                CreateDate = entity.CreateDate,
            };
        }

        public static TravelPlanEditVM ToEditVM(this TravelPlanEditDto dto)
        {
            return new TravelPlanEditVM
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                TravelDays = dto.TravelDays,
                CreateDate = dto.CreateDate,
            };
        }

        public static TravelPlanEditDto ToEditDto(this TravelPlanEditVM vm)
        {
            return new TravelPlanEditDto
            {
                Id = vm.Id,
                MemberId = vm.MemberId,
                TravelDays = vm.TravelDays,
                CreateDate = vm.CreateDate,
            };
        }

        public static TravelPlan ToEntity(this TravelPlanEditDto dto)
        {
            return new TravelPlan
            {
                Id = dto.Id,
                MemberId = dto.MemberId,
                TravelDays = dto.TravelDays,
                CreateDate = dto.CreateDate,
            };
        }
    }
}