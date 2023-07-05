using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IPaymentMethod
	{
		IEnumerable<PaymentMethodDto> Search();

		void Create(PaymentMethodCreateDto dto);

		void Edit(PaymentMethodEditDto dto);

		void Delete(int id);
		bool ExistPaymentMethod(string Name);
		PaymentMethodDto Get(int id);
		PaymentMethodEditDto GetEditDto(int id);

	}
}
