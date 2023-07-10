using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto
{
	public class MemberIdentityDto
	{
		public int Id { get; set; }

		public string Permission { get; set; }
		public MemberIdentityDto(int id, string permission)
		{
			this.Id = id;
			this.Permission = permission;
		}
	}
}