using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation.Service
{
	public class ServiceInfoDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool Selected { get; set; }
	}
}