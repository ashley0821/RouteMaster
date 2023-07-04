using Microsoft.Ajax.Utilities;
using RouteMaster.Models.Dto;
using RouteMaster.Models.ViewModels;
using RouteMaster.Models.ViewModels.Accommodations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class ActivitiesDetailsExts
	{

		public static ActivitiesDetailsIndexVM ToIndexVM(this ActivitiesDetailsDto dto)
		{
			return new ActivitiesDetailsIndexVM
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				ActivityId = dto.ActivityId,
				ActivityName = dto.ActivityName,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Price = dto.Price,
				Quantity = dto.Quantity,
			};
		}
		public static ActivitiesDetailsDto ToIndexDto(this ActivitiesDetailsIndexVM vm)
		{
			return new ActivitiesDetailsDto
			{
				Id = vm.Id,
				OrderId = vm.OrderId,
				ActivityId = vm.ActivityId,
				ActivityName = vm.ActivityName,
				StartTime = vm.StartTime,
				EndTime = vm.EndTime,
				Price = vm.Price,
				Quantity = vm.Quantity,
			};
		}
		public static ActivitiesDetailsEditVM ToEditVM(this ActivitiesDetailsEditDto dto)
		{
			return new ActivitiesDetailsEditVM
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				ActivityId = dto.ActivityId,
				ActivityName = dto.ActivityName,
				StartTime = dto.StartTime,
				EndTime = dto.EndTime,
				Price = dto.Price,
				Quantity = dto.Quantity,
			};
		}
		public static ActivitiesDetailsEditDto ToEditDto(this ActivitiesDetailsEditVM vm)
		{
			return new ActivitiesDetailsEditDto
			{
				Id = vm.Id,
				OrderId = vm.OrderId,
				ActivityId = vm.ActivityId,
				ActivityName = vm.ActivityName,
				StartTime = vm.StartTime,
				EndTime = vm.EndTime,
				Price = vm.Price,
				Quantity = vm.Quantity,
			};
		}
	}
}