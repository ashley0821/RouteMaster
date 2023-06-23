using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class ActivityEFRepositoy:IActivityRepository
	{
		private AppDbContext _db;

        public ActivityEFRepositoy()
        {
            _db = new AppDbContext();
        }

		public IEnumerable<ActivityIndexDto> Search()
		{
			//todo search 篩選條件設計

			return _db.Activities
			.AsNoTracking()
			.Include(a => a.ActivityCategory)
			.OrderBy(a => a.ActivityCategory.Id)
			.ToList()
			.Select(a => a.ToIndexDto());

		}
	}
}