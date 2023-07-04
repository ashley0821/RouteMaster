using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class Comments_AccommodationsService
	{
		private IComments_AccommodationsRepository _repo;

		public Comments_AccommodationsService(IComments_AccommodationsRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<Comments_AccommodationsIndexDto> Search()
		{
			return _repo.Search();
		}
	}
}