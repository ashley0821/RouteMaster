using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.ViewModels.Accommodations.Room
{
	public class AccommodationImagesVM
	{
        public string[] ImgName { get; set; }

        public HttpPostedFileBase[] Files { get; set; }
    }
}