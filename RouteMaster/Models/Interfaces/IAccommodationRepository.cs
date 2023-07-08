using RouteMaster.Models.Dto;
using RouteMaster.Models.Dto.Accommodation;
using RouteMaster.Models.Dto.Accommodation.Room;
using RouteMaster.Models.Infra;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using RouteMaster.Models.ViewModels.Accommodations.Room;
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
		void EditRoomProfile(RoomEditDto dto, ImagesDto IDto, string path);
		bool IsOriginalName(AccommodationEditDto dto);
		bool IsOriginalRoomName(RoomEditDto dto);
		void CreateRoomAndImages(RoomCreateDto dto, ImagesDto iDto, String path);
		void EditService(ServiceInfoVM vm);
		RoomEditDto GetRoomInfo(int? id);
		bool ExistRoomName(int accommodationId, string name);
	}
}
