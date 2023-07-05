using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<OrderIndexDto> Search(OrderCriteria criteria);
		OrderIndexVM GetOrderById(int id);


		void Edit(OrderEditDto order);
		void Delete(int id);
		
	
	}
    
}
