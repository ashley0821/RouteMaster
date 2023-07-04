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
	public interface IAccomodationDetailsRepository
	{
		

			IEnumerable<AccommodationDetailsDto> Search(int orderId);

			
		    void AccomodationDetailsEdit (AccomodationDetailsEditDto dto);

		    List<AccomodationDetailsVM> GetAccomodationDetails(int id);
			AccomodationDetailsEditDto GetAccomodationDetailsEditDetails(int id);


			void AccomodationDetailsDelete(int id);
		    AccomodationDetailsVM GetAccomodationDetailsById(int id);

	}
}
