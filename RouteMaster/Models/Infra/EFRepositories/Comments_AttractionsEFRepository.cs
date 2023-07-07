using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class Comments_AttractionsEFRepository:IComments_AttractionsRepository
	{
		private AppDbContext _db = new AppDbContext();

		public IEnumerable<Comments_AttractionsIndexDto> Search(Comments_AttractionCriteria criteria)
		{
			var commAttr = _db.Comments_Attractions.Include(c => c.Attraction).Include(c => c.Member);

			if(string.IsNullOrEmpty(criteria.AttractionName) == false)
			{
				commAttr= commAttr.Where(c => c.Attraction.Name.Contains(criteria.AttractionName));
			}
			if (criteria.SortId != null)
			{
				switch (criteria.SortId)
				{
					case 0:
						commAttr = commAttr.OrderBy(c => c.Id);
						break;
					case 1:
						commAttr = commAttr.OrderByDescending(c => c.Score);
						break;
					case 2:
						commAttr = commAttr.OrderByDescending(c => c.StayHours);
						break;
					case 3:
						commAttr = commAttr.OrderByDescending(c => c.Price);
						break;
					case 4:
						commAttr = commAttr.OrderByDescending(c => c.CreateDate);
						break;
				}
			}


			return commAttr.AsNoTracking()
				.ToList()
				.Select(c => c.ToIndexDto());
		}

		public bool ExistDetail(int? id)
		{
			return _db.Comments_Attractions.Any(c => c.Id == id);
		}
		public Comments_AttractionsDetailDto Detail(int? id)
		{
			var commAttrDb= _db.Comments_Attractions
				.Include(c => c.Attraction)
				 .FirstOrDefault(c => c.Id == id);

			var img = _db.Comments_AttractionImages
				.Where(i => i.Comments_AttractionId == id)
				.ToList()
				.Select(i => i.Image);

			Comments_AttractionsDetailDto dto = new Comments_AttractionsDetailDto
			{
				Id =(int)id,
				AttractioName = commAttrDb.Attraction.Name,
				Content = commAttrDb.Content,
				Score = commAttrDb.Score,
				Images = img
			};

			return dto;
		}

		public bool ExistImgWithinComment(int id)
		{
			return _db.Comments_AttractionImages.Any(i =>i.Comments_AttractionId == id);
		}

		public void ClearImg(int id)
		{
			var imgList = _db.Comments_AttractionImages.Where(i =>i.Comments_AttractionId == id).ToList();
			foreach(var img in imgList)
			{
				_db.Comments_AttractionImages.Remove(img);
				_db.SaveChanges();
			}
		}

		public void DeleteComment(int id)
		{
			Comments_Attractions comment = _db.Comments_Attractions.Find(id);
			_db.Comments_Attractions.Remove(comment);
			_db.SaveChanges();
		}
	}
}