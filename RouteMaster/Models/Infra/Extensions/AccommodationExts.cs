using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using RouteMaster.Models.ViewModels.Accommodations.Room;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class AccommodationExts
	{
		private readonly static AppDbContext _db = new AppDbContext(); 

		// dto轉vm
        public static AccommodationIndexVM ToVM(this AccommodationIndexDto dto)
		{
			return new AccommodationIndexVM
			{
				Id = dto.Id,
				PartnerId = dto.PartnerId,
				Name = dto.Name,
				Address = dto.Address,
				AccommodationImage = dto.AccommodationImage
			};
		}
        public static AccommodationEditVM ToVM(this AccommodationEditDto dto)
		{
			return new AccommodationEditVM
			{
				Id = dto.Id,
				PartnerId = dto.PartnerId,
				Name = dto.Name,
				Description = dto.Description,
				RegionId = dto.RegionId,
				TownId = dto.TownId,
				Address = dto.Address,
				PhoneNumber = dto.PhoneNumber,
				Website = dto.Website,
				IndustryEmail = dto.IndustryEmail,
				ParkingSpace = dto.ParkingSpace
			};
		}
		
		//vm轉dto
		public static AccommodationCreateDto ToDto(this AccommodationCreateVM vm)
		{
			return new AccommodationCreateDto
            {
				//Id = vm.Id,
				PartnerId = vm.PartnerId,
				Name = vm.Name,
                RegionId = vm.RegionId,
                TownId = vm.TownId,
                Address = vm.Address,
                PhoneNumber = vm.PhoneNumber,
                IndustryEmail = vm.IndustryEmail
            };
		}
		public static AccommodationEditDto ToDto(this AccommodationEditVM vm)
		{
			return new AccommodationEditDto
			{
				Id = vm.Id,
				PartnerId = vm.PartnerId,
				Name = vm.Name,
				Description = vm.Description,
				RegionId = vm.RegionId,
				TownId = vm.TownId,
				Address = vm.Address,
				PhoneNumber = vm.PhoneNumber,
				Website = vm.Website,
				IndustryEmail = vm.IndustryEmail,
				ParkingSpace = vm.ParkingSpace
			};
		}
		public static RoomCreateDto ToDto(this RoomCreateVM vm)
		{
			return new RoomCreateDto
			{

                AccommodationId = vm.AccommodationId,

				Type = vm.Type,
				Name = vm.Name,
				Quantity = vm.Quantity,
				Price = vm.Price
			};
		}


		//entity轉dto
		public static AccommodationIndexDto ToIndexDto(this Accommodation accommodation)
		{
			return new AccommodationIndexDto
			{
				Id = accommodation.Id,
				PartnerId = accommodation.PartnerId,
				Name = accommodation.Name,
				Address = accommodation.Address,
				AccommodationImage = accommodation.AccommodationImages.FirstOrDefault()?.Image
			};
		}
		public static AccommodationEditDto ToEditDto(this Accommodation accommodation)
		{
			int length = accommodation.Region.Name.Length + accommodation.Town.Name.Length;

			return new AccommodationEditDto
			{
				Id = accommodation.Id,
				PartnerId = accommodation.PartnerId,
				Name = accommodation.Name,
				Description = accommodation.Description,
				RegionId = accommodation.RegionId,
				TownId = accommodation.TownId,
				Address = accommodation.Address.Substring(length),
				PhoneNumber = accommodation.PhoneNumber,
				Website = accommodation.Website,
				IndustryEmail = accommodation.IndustryEmail,
				ParkingSpace = accommodation.ParkingSpace
			};
		}

		//dto 轉entity
		public static Accommodation ToIndexEntity(this AccommodationCreateDto dto)
		{
			string address = GetFullAddress(dto);
			return new Accommodation
			{
				//Id = dto.Id,
				PartnerId = 1,//dto.PartnerId,
				Name = dto.Name,
				RegionId = dto.RegionId,
				TownId = dto.TownId,
				Address = address,
				PhoneNumber = dto.PhoneNumber,
				IndustryEmail = dto.IndustryEmail,
                CreateDate = DateTime.Now
            };
		}
		#region
		//public static Accommodation ToEditEntity(this AccommodationEditDto dto)
		//{
		//	string address = GetFullAddress(dto);
		//	return new Accommodation
		//	{
		//		Id = dto.Id,
		//		PartnerId = dto.PartnerId,
		//		Name = dto.Name,
		//		Description = dto.Description,
		//		RegionId = dto.RegionId,
		//		TownId = dto.TownId,
		//		Address = address,
		//		PhoneNumber = dto.PhoneNumber,
		//		Website = dto.Website,
		//		IndustryEmail = dto.IndustryEmail,
		//		ParkingSpace = dto.ParkingSpace,
		//		CreateDate = DateTime.Now
		//	};
		//}
		#endregion
		//entity 轉vm
		public static RoomIndexVM ToVM(this Room entity)
		{
			return new RoomIndexVM
			{
				Id = entity.Id,
				AccommodationId = entity.AccommodationId,
				Name = entity.Name,
				Quantity = entity.Quantity,
			};
		}

		public static Room ToRoomCreateEntity(this RoomCreateDto dto)
		{
			return new Room
			{
				AccommodationId = dto.AccommodationId,//dto.PartnerId,
				Type = dto.Type,
				Name = dto.Name,
				Quantity = dto.Quantity,
				Price = dto.Price
			};
		}

		private static string GetFullAddress(AccommodationCreateDto dto)
		{
			return $"{_db.Regions.Where(r=>r.Id == dto.RegionId).FirstOrDefault().Name}{_db.Towns.Where(t=>t.Id == dto.TownId).FirstOrDefault().Name}{dto.Address}";
		}
		
		public static string GetFullAddress(this AccommodationEditDto dto)
		{
			return $"{_db.Regions.Where(r=>r.Id == dto.RegionId).FirstOrDefault().Name}{_db.Towns.Where(t=>t.Id == dto.TownId).FirstOrDefault().Name}{dto.Address}";
		}
	}
}