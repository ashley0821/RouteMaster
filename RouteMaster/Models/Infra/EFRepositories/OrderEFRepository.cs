using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
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

	 

		public IEnumerable<OrderIndexDto>Search(OrderCriteria criteria)
		{
			    


			var query = _db.Orders
		.AsNoTracking()
		.OrderByDescending(o => o.CreateDate)
		.Include(o => o.Member)
		.Include(o => o.PaymentMethod)
		.ToList();


			#region where

			if (!string.IsNullOrEmpty(criteria.MemberName))
			{
				query = query.Where(o => o.Member.FirstName == criteria.MemberName).ToList();
			}
			if(criteria.PaymentStatus !=null && criteria.PaymentStatus.Value > 0)
			{
				query = query.Where(o=>o.PaymentStatus==criteria.PaymentStatus).ToList();
			}
			
			#endregion



			var orders = query.Select(o => new OrderIndexDto
			{
				Id = o.Id,
				MemberId = o.MemberId,
				MemberName = o.Member.FirstName,
				PaymentMethodId = o.PaymentMethodId,
				PaymentMethodName = o.PaymentMethod.Name,
				PaymentStatus = o.PaymentStatus,
				CreateDate = o.CreateDate,
				Total = o.Total
			});

			return orders;
			//return (IEnumerable<OrderIndexDto>)_db.Orders
			//   .AsNoTracking()
			//   .OrderByDescending(o => o.CreateDate)
			//   .Include(o => o.Member)
			//   .Include(o => o.PaymentMethod)
			//   .ToList()
			//   //.Where(o=>o.Member.FirstName==criteria.MemberName)

			//   .Select(o => new OrderIndexDto
			//   {
			//	   Id = o.Id,
			//	   MemberId = o.MemberId,
			//	   MemberName = o.Member.FirstName,
			//	   PaymentMethodId = o.PaymentMethodId,
			//	   PaymentMethodName = o.PaymentMethod.Name,
			//	   PaymentStatus = o.PaymentStatus,
			//	   CreateDate = o.CreateDate,
			//	   Total = o.Total,

			//   });
		}

		
	}
}