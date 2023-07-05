using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class PaymentMethodService
	{
		private IPaymentMethod _repo;

		public PaymentMethodService(IPaymentMethod repo)
		{
			_repo = repo;
		}

		public IEnumerable<PaymentMethodDto> Search()
		{
			return _repo.Search();
		}
		
		public PaymentMethodDto Get(int id)
		{
			return _repo.Get(id);
		}

		public PaymentMethodEditDto GetEditDto(int id)
		{
			return _repo.GetEditDto(id);
		}
		public Result Create(PaymentMethodCreateDto dto)
		{
			_repo.Create(dto);
			return Result.Success();
		}

		public Result Edit(PaymentMethodEditDto dto)
		{
			_repo.Edit(dto);
			return Result.Success();
		}

		public Result Delete(int id)
		{
			_repo.Delete(id);
			return Result.Success();
		}
	}
}