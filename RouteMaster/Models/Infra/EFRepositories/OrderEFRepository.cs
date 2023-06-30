using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class OrderEFRepository : IOrderRepository
	{
		private readonly AppDbContext _db = new AppDbContext();

	 

		IEnumerable<OrderIndexDto> IOrderRepository.Search()
		{
			return (IEnumerable<OrderIndexDto>)_db.Orders
			   .AsNoTracking()
			   .OrderByDescending(o => o.CreateDate)
			   .Include(o => o.Member)
			   .Include(o => o.PaymentMethod)
			   .Select(o => new OrderIndexDto
			   {
				   Id = o.Id,
				   MemberId = o.MemberId,
				   MemberName = o.Member.FirstName,
				   PaymentMethodId = o.PaymentMethodId,
				   PaymentMethodName = o.PaymentMethod.Name,
				   PaymentStatus = o.PaymentStatus,
				   CreateDate = o.CreateDate,
				   Total = o.Total,

			   });
		}

		
	}
}