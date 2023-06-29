using RouteMaster.Models.Dto;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace RouteMaster.Models.Interfaces
{
	public interface IActivitiesDetailsRepository
	{
		IEnumerable<ActivitiesDetailsDto> Search();

		void Create(ActivitiesDetailsDto dto);

		void Edit(ActivitiesDetailsDto dto);

		void Delete(int id);

	}
}
