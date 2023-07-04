using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation.Service
{
	public class ServiceInfoDto
	{
        public int AccommodationId { get; set; }
        public List<ServiceInfo> ServiceInfoList { get; set; }
    }
}