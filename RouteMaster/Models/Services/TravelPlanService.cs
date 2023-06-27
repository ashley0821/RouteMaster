using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace RouteMaster.Models.Services
{
    public class TravelPlanService
    {
        private ITravelPlanRepository _repo;


        public TravelPlanService(ITravelPlanRepository reop)
        {
            _repo = reop;
            
        }

        public IEnumerable<TravelPlanIndexDto> Search()
        {
            return _repo.Search();
        }

        public Result Create(TravelPlanCreateDto dto)
        {
            _repo.Create(dto);
            return Result.Success();
        }

        public Result Edit(TravelPlanEditDto dto)
        {
            _repo.Edit(dto);
            return Result.Success();

        }

        public Result Delete(int id)
        {
            _repo.Delete(id);
            return Result.Success();
        }
    }
}