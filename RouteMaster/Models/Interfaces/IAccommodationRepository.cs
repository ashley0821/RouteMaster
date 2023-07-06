using RouteMaster.Models.Dto;
using RouteMaster.Models.Dto.Accommodation;
using RouteMaster.Models.Infra;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RouteMaster.Models.Interfaces
{
	public interface IAccommodationRepository
	{
		IEnumerable<AccommodationIndexDto> Search(int? id);
		void Create(AccommodationCreateDto dto);
        bool ExistName(string name);
		AccommodationEditDto GetEditInfo(int? id);
		void EditAccommodationProfile(AccommodationEditDto dto, ImagesDto IDto, string path);
		bool IsOriginalName(AccommodationEditDto dto);
		void CreateRoomAndImages(RoomCreateDto dto, ImagesDto iDto, String path);
		void EditService(ServiceInfoVM vm);
	}
}
