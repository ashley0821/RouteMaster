using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Dto.Accommodation
{
	public class ImagesDto
	{
        public string[] ImgName { get; set; }
            
        public HttpPostedFileBase[] Files { get; set; }
    }
}