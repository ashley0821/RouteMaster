using RouteMaster.Models.Dto.Accommodation.Service;
using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations
{
	public class ServiceInfoVM
	{
		public int AccommodationId { get; set; }
		public List<ServiceInfoDto> ServiceInfoList { get; set; }
	}
}