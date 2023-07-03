using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations.Room
{

	public class RoomType
	{
        public int Id { get; set; }
        public string Type { get; set; }
        public RoomType(int id, string type)
        {
            this.Type = type;
            this.Id = id;
        }
    }
}