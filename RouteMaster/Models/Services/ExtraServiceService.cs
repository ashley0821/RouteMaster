using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class ExtraServiceService
	{
		private IExtraServiceRepository _repo;

        public ExtraServiceService(IExtraServiceRepository repo)
        {
            _repo = repo;            
        }

        public IEnumerable<ExtraServiceIndexDto> Search()
        {
            return _repo.Search();
        }


        public Result Create(ExtraServiceCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Result Edit(ExtraServiceEditDto dto)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotSupportedException();
        }




    }
}