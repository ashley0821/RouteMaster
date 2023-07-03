using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
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

        public Result Create(AccommodationDetailsDto dto)
		{
			_repo.Create(dto);
			return Result.Success();
		}

		public Result Edit(AccommodationDetailsDto dto)
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