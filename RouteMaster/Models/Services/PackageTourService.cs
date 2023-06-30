using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
    public class PackageTourService
    {
        private IPackageTourRepository _repo;

        public PackageTourService(IPackageTourRepository repo)
        {
            _repo = repo; 
        }

        public IEnumerable<PackageTourIndexDto> Search()
        {
            return _repo.Search();
        }


        public Result Create(PackageTourCreateDto dto)
        {

            _repo.Create(dto);
            return Result.Success();
        }

    }
}