using RouteMaster.Models.Dto;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class AccomodationDetailsExts
	{
			
		public static AccomodationDetailsVM ToIndexVM(this AccommodationDetailsDto dto)
		{
			return new AccomodationDetailsVM
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				AccommodationId=dto.AccommodationId,
				AccommodationName=dto.AccommodationName,
				RoomName=dto.RoomName,
				RoomType=dto.RoomType,
				CheckIn=dto.CheckIn,
				CheckOut=dto.CheckOut,
				RoomPrice=dto.RoomPrice,


			};
		}



		public static AccommodationDetailsDto ToIndexDto(this AccomodationDetailsVM vm)
		{
			return new AccommodationDetailsDto
			{
				Id = vm.Id,
				OrderId = vm.OrderId,
				AccommodationId = vm.AccommodationId,
				AccommodationName = vm.AccommodationName,
				RoomName = vm.RoomName,
				RoomType = vm.RoomType,
				CheckIn = vm.CheckIn,
				CheckOut = vm.CheckOut,
				RoomPrice = vm.RoomPrice,
			};
		}


		public static AccomodationDetailsEditDto ToEditDto(this AccomodationDetailsEditVM vm)
		{
			return new AccomodationDetailsEditDto
			{
				Id = vm.Id,
				OrderId = vm.OrderId,
				AccommodationId = vm.AccommodationId,
				AccommodationName = vm.AccommodationName,
				RoomName = vm.RoomName,
				RoomType = vm.RoomType,
				CheckIn = vm.CheckIn,
				CheckOut = vm.CheckOut,
				RoomPrice = vm.RoomPrice,
			};
		}

		public static AccomodationDetailsEditVM ToEditVM(this AccomodationDetailsEditDto dto)
		{
			return new AccomodationDetailsEditVM
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				AccommodationId = dto.AccommodationId,
				AccommodationName = dto.AccommodationName,
				RoomName = dto.RoomName,
				RoomType = dto.RoomType,
				CheckIn = dto.CheckIn,
				CheckOut = dto.CheckOut,
				RoomPrice = dto.RoomPrice,
			};
		}
	}
}