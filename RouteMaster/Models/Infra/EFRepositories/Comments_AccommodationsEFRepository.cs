using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class Comments_AccommodationsEFRepository : IComments_AccommodationsRepository
	{
		private AppDbContext _db = new AppDbContext();
		public IEnumerable<Comments_AccommodationsIndexDto> Search()
		{
			var commAccDb = _db.Comments_Accommodations.Include(c => c.Accommodation).Include(c => c.Member);

			return commAccDb.AsNoTracking()
				.ToList()
				.Select(c => c.ToIndexDto());
		}	
	}
}