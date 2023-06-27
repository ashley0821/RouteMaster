using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
    public interface IPackageTourRepository
    {
		IEnumerable<PackageTourIndexDto> Search();

		void Create(PackageTourCreateDto dto);

		void Edit(PackageTourEditDto dto);

		void Delete(int id);
    }
}
