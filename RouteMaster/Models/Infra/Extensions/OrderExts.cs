using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
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
				PaymentMethodName = entity.PaymentMethod.Name,
				PaymentStatus = entity.PaymentStatus,
				CreateDate = entity.CreateDate,
				Total = entity.Total,
			};
		}
	}
}