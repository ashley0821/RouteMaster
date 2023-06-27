using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
    public interface ITravelPlanRepository
    {

        IEnumerable<TravelPlanIndexDto> Search();

        void Create(TravelPlanCreateDto dto);

        void Edit(TravelPlanEditDto dto);

        void Delete(int id);
    }
}
