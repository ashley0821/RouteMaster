using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.Extensions
{
	public static class OrderExts
	{
		public static OrderIndexVM ToIndexVM(this OrderIndexDto dto)
		{
			return new OrderIndexVM
			{
				Id = dto.Id,
				//MemberId = dto.MemberId,
				MemberName = dto.MemberName,
				MemberEmail = dto.MemberEmail,
				//PaymentMethodId = dto.PaymentMethodId,
				PaymentMethodName = dto.PaymentMethodName,
				PaymentStatus = dto.PaymentStatus,
				CreateDate = dto.CreateDate,
				Total = dto.Total,

			};
		}




		public static OrderIndexDto ToIndexDto(this Order entity)
		{
			return new OrderIndexDto
			{
				Id = entity.Id,
				//MemberId = entity.MemberId,
				MemberName = entity.Member.FirstName,
				//PaymentMethodId = entity.PaymentMethodId,
				MemberEmail= entity.Member.Email,
				PaymentMethodName = entity.PaymentMethod.Name,
				PaymentStatus = entity.PaymentStatus,
				CreateDate = entity.CreateDate,
				Total = entity.Total,
			};
		}

		public static OrderEditDto ToIndexDto(this OrderEditVM vm)
		{
			return new OrderEditDto
			{
				Id = vm.Id,
				MemberId = vm.MemberId,
				MemberName = vm.MemberName,
				MemberEmail = vm.MemberEmail,
				PaymentMethodId = vm.PaymentMethodId,
				PaymentMethodName = vm.PaymentMethodName,
				CreateDate = vm.CreateDate,
				Total = vm.Total,
			};
		}

		public static Order ToEntity(this OrderIndexDto dto)
		{
			return new Order
			{
				Id = dto.Id,
				//MemberId = dto.MemberId,
				MemberId = dto.MemberId,
				//PaymentMethodId = dto.PaymentMethodId,
				PaymentStatus = dto.PaymentStatus,
				CreateDate = dto.CreateDate,
				Total = dto.Total,
			};
		}

		public static OrderEditDto ToEditDto(this Order entity)
		{
			return new OrderEditDto
			{
				Id = entity.Id,
				MemberId = entity.MemberId,
				MemberName = entity.Member.FirstName,
				PaymentMethodId = entity.PaymentMethodId,
				MemberEmail = entity.Member.Email,
				PaymentMethodName = entity.PaymentMethod.Name,
				PaymentStatus = entity.PaymentStatus,
				CreateDate = entity.CreateDate,
				Total = entity.Total,
			};
		}

		public static OrderEditVM ToEditVM(this OrderEditDto dto)
		{
			return new OrderEditVM
			{
				Id = dto.Id,
				MemberId = dto.MemberId,
				MemberName = dto.MemberName,
				TravelPlanId = dto.TravelPlanId,
				MemberEmail = dto.MemberEmail,
				PaymentMethodId = dto.PaymentMethodId,
				PaymentMethodName = dto.PaymentMethodName,
				PaymentStatus = dto.PaymentStatus,
				CreateDate = dto.CreateDate,
				Total = dto.Total,
			};
		}

		public static OrderEditDto ToEditDto(this OrderEditVM vm)
		{
			return new OrderEditDto
			{
				Id = vm.Id,
				MemberId = vm.MemberId,
				TravelPlanId=vm.TravelPlanId,
				MemberName = vm.MemberName,
				MemberEmail = vm.MemberEmail,
				PaymentMethodId = vm.PaymentMethodId,
				PaymentMethodName = vm.PaymentMethodName,
				CreateDate = vm.CreateDate,
				PaymentStatus=vm.PaymentStatus,
				Total = vm.Total,
			};
		}
		public static Order ToEntity(this OrderEditDto dto)
		{
			return new Order
			{
				Id = dto.Id,
				//MemberId = dto.MemberId,
				MemberId = dto.MemberId,
				//PaymentMethodId = dto.PaymentMethodId,
				PaymentStatus = dto.PaymentStatus,
				CreateDate = dto.CreateDate,
				Total = dto.Total,
			};
		}
	}
}