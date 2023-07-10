using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Infra.Criterias
{
	public class AdministratorCriteria
	{
		public int? PermissionId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public DateTime? CreateDateBegin { get; set; }

		public DateTime? CreateDateEnd { get; set; }

		public string IsSuspended { get; set; }

	}
}