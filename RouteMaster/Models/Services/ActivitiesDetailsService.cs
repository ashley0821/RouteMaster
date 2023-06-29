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
	public class ActivitiesDetailsService
	{
		private IActivitiesDetailsRepository _repo;


		public ActivitiesDetailsService(IActivitiesDetailsRepository reop)
		{
			_repo = reop;

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

		public Result Edit(ActivitiesDetailsDto dto)
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
