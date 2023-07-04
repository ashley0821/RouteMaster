using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Services;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IExtraServiceDetailsRepository
	{
		IEnumerable<ExtraServicesDetailsDto> Search();

		void Create(ExtraServicesDetailsDto dto);

		void ExtraServicesDetailsEdit(ExtraServicesDetailsEditDto dto);

		void ExtraServicesDetailsDelete(int id);
		ExtraServicesDetailsVM GetExtraServicesDetailsById(int id);
		List<ExtraServicesDetailsVM> GetExtraServicesDetails(int id);
		ExtraServicesDetailsEditDto GetExtraServicesEditDetails(int id);
	}

}
