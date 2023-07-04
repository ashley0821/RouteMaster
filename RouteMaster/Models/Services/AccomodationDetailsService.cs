using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class AccomodationDetailsService
	{
		private IAccomodationDetailsRepository _repo;


		public AccomodationDetailsService(IAccomodationDetailsRepository repo)
		{
			_repo = repo;

		}

        public IEnumerable<AccommodationDetailsDto> Search(int orderId)
        {
            return _repo.Search(orderId);
        }

      
		public Result AccomodationDetailsEdit(AccomodationDetailsEditDto dto)
		{
			_repo.AccomodationDetailsEdit(dto);
			return Result.Success();
		}

		public Result AccomodationDetailsDelete(int id)
		{
			_repo.AccomodationDetailsDelete(id);
			return Result.Success();
		}

		public AccomodationDetailsEditVM GetAccomodationDetailsEditDetails(int id)
		{
			return _repo.GetAccomodationDetailsEditDetails(id).ToEditVM();
		}
		public AccomodationDetailsVM GetAccomodationDetailsById(int id)
		{
			var accommodationDetailInDb = _repo.GetAccomodationDetailsById(id);
			return accommodationDetailInDb;
		}
	}
}