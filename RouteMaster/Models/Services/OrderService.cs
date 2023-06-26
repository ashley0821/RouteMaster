using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class OrderService
	{


		private IOrderRepository _repo;

		public OrderService(IOrderRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<OrderIndexDto> Search()
		{
			return _repo.Search();
		}

	}
}