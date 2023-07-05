using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
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

namespace RouteMaster.Models.Services
{
	public class OrderService
	{


        private IOrderRepository _repo;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
        

        public IEnumerable<OrderIndexDto> Search(OrderCriteria criteria)
        {
            return _repo.Search(criteria);
        }
		//public  Add(OrderIndexDto dto)
		//{
		//	_repo.Add(order)
		//}

		public Result Delete(int id)
		{
			_repo.Delete(id); 
			return Result.Success();
		}

		public OrderIndexVM GetOrderById(int id)
		{
			return _repo.GetOrderById(id);
		}

		public Result Edit(OrderEditDto dto)
		{
			_repo.Edit(dto);
			return Result.Success();

		}

	

	}
}