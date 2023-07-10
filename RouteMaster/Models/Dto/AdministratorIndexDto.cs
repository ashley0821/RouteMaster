using RouteMaster.Models.EFModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace RouteMaster.Models.Dto
{
	public class AdministratorIndexDto
	{
		public int Id { get; set; }

		public int PermissionId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public DateTime? CreateDate { get; set; }

		public virtual Permission Permission { get; set; }

        public bool? IsSuspended { get; set; }
    }
}