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
	public class ExtraServicesDetailsService
	{
		private IExtraServiceDetailsRepository _repo;


		public ExtraServicesDetailsService(IExtraServiceDetailsRepository repo)
		{
			_repo = repo;

		}
	


		public IEnumerable<ExtraServicesDetailsDto> Search(int orderId)
		{
			return _repo.Search();
		}

		public Result Create(ExtraServicesDetailsDto dto)
		{
			_repo.Create(dto);
			return Result.Success();
		}

		//public Result Edit(ExtraServicesDetailsEditDto dto)
		//{
		//	_repo.Edit(dto);
		//	return Result.Success();

		//}
		public Result ExtraServicesDetailsEdit(ExtraServicesDetailsEditDto dto)
		{
			_repo.ExtraServicesDetailsEdit(dto);
			return Result.Success();

		}


		public Result Delete(int id)
		{
			_repo.Delete(id);
			return Result.Success();
		}
		public ExtraServicesDetailsEditVM GetExtraServicesEditDetails(int id)
		{

			return _repo.GetExtraServicesEditDetails(id).ToEditVM();
		}


	}

}