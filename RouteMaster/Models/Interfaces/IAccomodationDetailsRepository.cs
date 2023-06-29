using RouteMaster.Models.Dto;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IAccomodationDetailsRepository
	{
		
			IEnumerable<AccommodationDetailsDto> Search();

			void Create(AccommodationDetailsDto dto);

			void Edit(AccommodationDetailsDto dto);

			void Delete(int id);
		
	}
}
