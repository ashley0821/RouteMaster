using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
    public class TravelPlanDapperRepository : ITravelPlanRepository
    {
        public void Create(TravelPlanCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(TravelPlanEditDto dto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TravelPlanIndexDto> Search()
        {
            throw new NotImplementedException();
        }
    }
}