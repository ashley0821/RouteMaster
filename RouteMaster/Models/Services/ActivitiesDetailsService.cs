using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class ActivitiesDetailsService
	{
		private IActivitiesDetailsRepository _repo;


		public ActivitiesDetailsService(IActivitiesDetailsRepository repo)
		{
			_repo = repo;

		}

		public IEnumerable<ActivitiesDetailsDto> Search()
		{
			return _repo.Search();
		}

		public Result Create(ActivitiesDetailsDto dto)
		{
			_repo.Create(dto);
			return Result.Success();
		}

		public Result ActivitiesDetailsEdit(ActivitiesDetailsEditDto dto)
		{
			_repo.ActivitiesDetailsEdit(dto);
			return Result.Success();

		}

		public Result ActivitiesDetailsDelete(int id)
		{
			_repo.ActivitiesDetailsDelete(id);
			return Result.Success();
		}

		public ActivitiesDetailsIndexVM GetActivitiesDetailsById(int id)
		{
			var activitiesDetailsInDb = _repo.GetActivitiesDetailsById(id);
			return activitiesDetailsInDb;
		}
		public ActivitiesDetailsEditVM GetActivitiesDetailsEditDetails(int id)
		{
			return _repo.GetActivitiesDetailsEditDetails(id).ToEditVM();
		}
		
	}
}
