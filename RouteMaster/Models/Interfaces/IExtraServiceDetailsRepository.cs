using RouteMaster.Models.Dto;
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

		void Edit(ExtraServicesDetailsDto dto);

		void Delete(int id);
	}
}
