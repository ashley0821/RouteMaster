using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations.Room
{
	public class RoomImagesVM
	{
		public string[] imgName { get; set; }

		public HttpPostedFileBase[] files { get; set; }
	}
}