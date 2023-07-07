using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class OrderEFRepository : IOrderRepository
	{
		private readonly AppDbContext _db = new AppDbContext();




		public IEnumerable<OrderIndexDto> Search(OrderCriteria criteria)
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
			if (criteria.PaymentStatus != null && criteria.PaymentStatus.Value > 0)
			{
				query = query.Where(o => o.PaymentStatus == criteria.PaymentStatus).ToList();
			}
			if (criteria.CreateStartDate.HasValue)
			{
				DateTime startDate = criteria.CreateStartDate.Value;
				query = query.Where(o => o.CreateDate >= startDate).ToList();
			}

			if (criteria.CreateEndDate.HasValue)
			{
				DateTime endDate = criteria.CreateEndDate.Value;
				query = query.Where(o => o.CreateDate <= endDate).ToList();
			}
			#endregion



			var orders = query.Select(o => new OrderIndexDto
			{
				Id = o.Id,
				MemberId = o.MemberId,
				MemberName = $"{o.Member.FirstName} {o.Member.LastName}",
				MemberEmail=o.Member.Email,

				PaymentMethodId = o.PaymentMethodId,
				PaymentMethodName = o.PaymentMethod.Name,
				PaymentStatus = o.PaymentStatus,
				CreateDate = o.CreateDate,
				Total = o.Total
			});;

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

		void IOrderRepository.Delete(int id)
		{
			Order order = _db.Orders.Find(id);
			try
			{
				_db.Orders.Remove(order);
				_db.SaveChanges();
			}
			catch (Exception ex)
			{
				throw new Exception("無法刪除", ex);
			}
		}

		void IOrderRepository.Edit(OrderEditDto dto)
		{
			Order order = _db.Orders.Find(dto.Id);

			order.MemberId = dto.MemberId;
			order.Member.FirstName = dto.MemberName;
			order.Member.LastName = dto.MemberName;
			order.Member.Email = dto.MemberEmail;
			order.PaymentMethod.Name = dto.PaymentMethodName;
			order.PaymentStatus = dto.PaymentStatus;
			order.CreateDate = dto.CreateDate;
			order.Total = dto.Total;

			_db.SaveChanges();
			
		}

		OrderIndexVM IOrderRepository.GetOrderById(int id)
		{
			return _db.Orders
	   .AsNoTracking()
	   .Where(o => o.Id == id)
	   .Select(o => new OrderIndexVM
	   {
		   Id = o.Id,
		   MemberId = o.MemberId,
		   MemberName = $"{o.Member.FirstName} {o.Member.LastName}",
		   MemberEmail= $"{o.Member.Email}",
		   PaymentMethodName = o.PaymentMethod.Name,
		   PaymentStatus = o.PaymentStatus,
		   CreateDate = o.CreateDate,
		   Total = o.Total,

	   }).FirstOrDefault();
		}

		
	}
}