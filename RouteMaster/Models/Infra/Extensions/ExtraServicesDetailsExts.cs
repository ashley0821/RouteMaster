using Microsoft.Ajax.Utilities;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class ExtraServicesDetailsExts
	{
		public static ExtraServicesDetailsVM ToIndexVm(this ExtraServicesDetailsDto dto)
		{
			return new ExtraServicesDetailsVM
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				ExtraServiceId = dto.ExtraServiceId,
				Price = dto.Price,
				Quantity = dto.Quantity,

			};
		}
		public static ExtraServicesDetailsDto ToIndexDto(this ExtraServicesDetailsVM vm) 
		{
			return new ExtraServicesDetailsDto
			{
				Id = vm.Id,
				OrderId = vm.OrderId,
				ExtraServiceId = vm.ExtraServiceId,
				Price = vm.Price,
				Quantity = vm.Quantity,
			};
		}
		public static ExtraServicesDetail ToEntity(this ExtraServicesDetailsDto dto)
		{
			return new ExtraServicesDetail
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				ExtraServiceId = dto.ExtraServiceId,
				Price = dto.Price,
				Quantity = dto.Quantity,

			};
		}
		public static ExtraServicesDetailsDto ToIndexDto(this ExtraServicesDetail entity)
		{
			return new ExtraServicesDetailsDto
			{
				Id = entity.Id,
				OrderId = entity.OrderId,
				ExtraServiceId = entity.ExtraServiceId,
				Price = entity.Price,
				Quantity = entity.Quantity,

			};
		}
		public static ExtraServicesDetailsEditDto ToEditDto(this ExtraServicesDetail entity)
		{
			return new ExtraServicesDetailsEditDto
			{
				Id = entity.Id,
				OrderId = entity.OrderId,
				ExtraServiceId = entity.ExtraServiceId,
				Price = entity.Price,
				Quantity = entity.Quantity,
			};
		}
		public static ExtraServicesDetailsEditVM ToEditVM(this ExtraServicesDetailsEditDto dto)
		{
			return new ExtraServicesDetailsEditVM
			{
				Id = dto.Id,
				OrderId = dto.OrderId,
				ExtraServiceId= dto.ExtraServiceId,
				ExtraServiceName = dto.ExtraServiceName,
				Price = dto.Price,
				Quantity = dto.Quantity,

			};
		}
		public static ExtraServicesDetailsEditDto ToEditDto(this ExtraServicesDetailsEditVM vm)
		{
			return new ExtraServicesDetailsEditDto
			{
				Id = vm.Id,
				OrderId= vm.OrderId,
				ExtraServiceId= vm.ExtraServiceId,
				ExtraServiceName = vm.ExtraServiceName,
				Price = vm.Price,
				Quantity = vm.Quantity,
			};
		}
	}

}