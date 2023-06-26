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
	public class ActivityEFRepository:IActivityRepository
	{
		private AppDbContext _db;

        public ActivityEFRepository()
        {
            _db = new AppDbContext();
        }

		public void Create(ActivityCreateDto dto)
		{
			Activity activity= dto.ToEntity();
			_db.Activities.Add(activity);
			_db.SaveChanges();
		}

		public void Delete(int id)
		{
			var activity = _db.Activities.Find(id);
			_db.Activities.Remove(activity);
			_db.SaveChanges();
		}

		public void Edit(ActivityEditDto dto)
		{
			var activityInDb=_db.Activities.Find(dto.Id);

			activityInDb.ActivityCategoryId = dto.ActivityCategoryId;
			activityInDb.AttractionId = dto.AttractionId;
			activityInDb.Name=dto.Name;
			activityInDb.RegionId = dto.RegionId;			
			activityInDb.Price=dto.Price;
			activityInDb.StartTime=dto.StartTime;
			activityInDb.EndTime=dto.EndTime;
			activityInDb.Description = dto.Description;
			activityInDb.Status=dto.Status;

			_db.SaveChanges();
			
			
		}

		public bool ExistAcativity(string activityName, int attractionId, DateTime startTime, DateTime endTime)
		{
			return _db.Activities.
			Any(a => a.Name == activityName 
				&& a.AttractionId == attractionId 
				&& a.StartTime == startTime 
				&& a.EndTime == endTime);
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