using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RouteMaster.Models.Interfaces
{
	public interface IComments_AccommodationsRepository
	{
		IEnumerable<Comments_AccommodationsIndexDto> Search	();

		void Create(Comments_AccommodationsCreateDto dto, HttpPostedFileBase[] file1, string path);
	}
}
