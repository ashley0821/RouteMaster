using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class AccommodationService
	{
		private readonly IAccommodationRepository _repo;

		public AccommodationService(IAccommodationRepository repo)
		{
			this._repo = repo;
		}

		public IEnumerable<AccommodationIndexDto> Search()
		{
			return _repo.Search();
		}
	}
}