using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class PaymentMethodEFRepository:IPaymentMethod
	{
		private AppDbContext _db;
		public PaymentMethodEFRepository() 
		{
			_db = new AppDbContext();
		}

		public IEnumerable<PaymentMethodDto> Search()
		{
			var paymentMethods = _db.PaymentMethods.AsNoTracking().ToList();
			return paymentMethods.Select(p => p.ToIndexDto());
		}
		public void Create(PaymentMethodCreateDto dto)
		{
			PaymentMethod paymentmethod = new PaymentMethod
			{
				Id = dto.id,
				Name = dto.Name,
				Description = dto.Description,
			};

			_db.PaymentMethods.Add(paymentmethod);
			_db.SaveChanges(); 
		}

		public void Edit(PaymentMethodEditDto dto)
		{
			PaymentMethod paymentMethod = _db.PaymentMethods.Find(dto.id);
			paymentMethod.Name = dto.Name;
			paymentMethod.Description = dto.Description;

			_db.SaveChanges();
		}

		public void Delete(int id)
		{
			PaymentMethod paymentMethod=_db.PaymentMethods.Find(id);
			try
			{
				_db.PaymentMethods.Remove(paymentMethod);
				_db.SaveChanges();
			}
			catch(Exception ex)
			{
				throw new Exception("很抱歉，無法刪除", ex);
			}
		}

		public bool ExistPaymentMethod(string Name)
		{
			return _db.PaymentMethods.Any(p=> p.Name == Name);	
		}

		public PaymentMethodDto Get(int id)
		{
			return _db.PaymentMethods
				.AsNoTracking()
				.Where(p => p.Id == id)
				.Select(p => new PaymentMethodDto
				{
					id = p.Id,
					Name = p.Name,
					Description = p.Description,
				}).FirstOrDefault();
		}

		public PaymentMethodEditDto GetEditDto(int id)
		{
			return _db.PaymentMethods
				.Where(p=> p.Id == id)
				.Select(p=> new PaymentMethodEditDto
				{
					id=p.Id,
					Name = p.Name,
					Description = p.Description,
				}).FirstOrDefault(); 
		}
	}
}