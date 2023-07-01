using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IAccommodationRepository
	{
		IEnumerable<AccommodationIndexDto> Search();
		void Create(AccommodationCreateDto dto);
        bool ExistName(string name);
		AccommodationEditDto GetEditInfo(int? id);
		void EditAccommodationProfile(AccommodationEditDto dto);
		bool IsOriginalName(AccommodationEditDto dto);
	}
}
