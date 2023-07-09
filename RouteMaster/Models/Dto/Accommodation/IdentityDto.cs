using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation
{
	public class IdentityDto
	{
        public string Email { get; set; }
        public int Id { get; set; }
        public IdentityDto(string email, int id)
        {
            this.Email = email;
            this.Id = id;
        }
    }
}