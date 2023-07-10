using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations.Room
{

	public class RoomType
	{
        public string Value { get; set; }
        public string Text { get; set; }
        public RoomType(string value, string type)
        {
            this.Text = type;
            this.Value = value;
        }
    }
}