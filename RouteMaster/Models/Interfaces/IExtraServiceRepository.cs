using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IExtraServiceRepository
	{
		IEnumerable<ExtraServiceIndexDto> Search();

		void Create(ExtraServiceCreateDto dto);

		bool ExistExtraService(string name, int attractionId);	

		void Edit(ExtraServiceEditDto dto);
		
		void Delete(int id);	

		ExtraServiceEditDto GetExtraServiceById(int id);

	}
}
