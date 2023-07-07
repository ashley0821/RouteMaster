using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using RouteMaster.Models.Infra.Criterias;

namespace RouteMaster.Models.Infra.EFRepositories
{
	public class Comments_AccommodationsEFRepository : IComments_AccommodationsRepository
	{
		private AppDbContext _db = new AppDbContext();

		public IEnumerable<Comments_AccommodationsIndexDto> Search(Comments_AccommodationCriteria criteria)
		{

			var commAccDb = _db.Comments_Accommodations.Include(c => c.Accommodation).Include(c => c.Member);

			if(string.IsNullOrEmpty(criteria.AccomodationName)== false)
			{
				commAccDb = commAccDb.Where(c => c.Accommodation.Name.Contains(criteria.AccomodationName));
			}
			if(string.IsNullOrEmpty(criteria.Title)== false)
			{
				commAccDb = commAccDb.Where(c => c.Title.Contains(criteria.Title));
			}
			if(criteria.SortId != null)
			{
				switch(criteria.SortId)
				{
					case 0:
						commAccDb = commAccDb.OrderBy(c => c.Id);
						break;
					case 1:
						commAccDb = commAccDb.OrderByDescending(c => c.Score);
						break;
					case 2:
						commAccDb = commAccDb.OrderByDescending(c => c.CreateDate);
						break;
				}
			}
			return commAccDb.AsNoTracking()
				.ToList()
				.Select(c => c.ToIndexDto());
		}

		public void Create(Comments_AccommodationsCreateDto dto, HttpPostedFileBase[] file1, string path)
		{
			var memberId = _db.Members.Where(m => m.Account == dto.MemberAccount).Select(m => m.Id).FirstOrDefault();

			Comments_Accommodations commAccDb = new Comments_Accommodations
			{
				MemberId = memberId,
				AccommodationId = dto.AccomodationId,
				Score = dto.Score,
				Title = dto.Title,
				Pros = dto.Pros,
				Cons = dto.Cons,
				CreateDate = DateTime.Now

			};

			var cA = _db.Comments_Accommodations;
			cA.Add(commAccDb);




			Comments_AccommodationImages img = new Comments_AccommodationImages();

				foreach(var i in file1)
				{
					if (i != null)
					{
						string fileName = SaveUploadedFile(path, i);
						img.Image = fileName;
						_db.Comments_AccommodationImages.Add(img);
						_db.SaveChanges();
					}
					else
					{
						_db.SaveChanges();
					}
				}
			
		}


		private string SaveUploadedFile(string path, HttpPostedFileBase file1)
		{
			// 如果沒有上傳檔案或檔案是空的,就不處理, 傳回 string.empty
			if (file1 == null || file1.ContentLength == 0) return string.Empty;

			// 取得上傳檔案的副檔名
			string ext = System.IO.Path.GetExtension(file1.FileName); // ".jpg" 而不是 "jpg"

			// 如果副檔名不在允許的範圍裡,表示上傳不合理的檔案類型, 就不處理, 傳回 string.empty
			string[] allowedExts = new string[] { ".jpg", ".jpeg", ".png", ".tif" };
			if (allowedExts.Contains(ext.ToLower()) == false) return string.Empty;

			// 生成一個不會重複的檔名
			string newFileName = Guid.NewGuid().ToString("N") + ext; // 生成 亂碼.jpg
			string fullName = System.IO.Path.Combine(path, newFileName);

			// 將上傳檔案存放到指定位置
			file1.SaveAs(fullName);

			// 傳回存放的檔名
			return newFileName;
		}

		public void Update(Comments_AccommodationsEditDto dto)
		{
			var commAccDb = _db.Comments_Accommodations.Find(dto.Id);

			commAccDb.Title = dto.Title;
			commAccDb.Pros=dto.Pros;
			commAccDb.Cons=dto.Cons;
			commAccDb.Score=dto.Score;

			_db.SaveChanges();
		}

		public bool ExistImgWithinComment(int id)
		{
			return _db.Comments_AccommodationImages.Any(i =>i.Comments_AccommodationId == id);
		}

		public void ClearImg(int id)
		{
			var imgList=_db.Comments_AccommodationImages.Where(i => i.Comments_AccommodationId==id).ToList();
			foreach (var img in imgList)
			{
				_db.Comments_AccommodationImages.Remove(img);
				_db.SaveChanges();
			}
		}

		public void DeleteComment(int id)
		{
			Comments_Accommodations comment = _db.Comments_Accommodations.Find(id);
			_db.Comments_Accommodations.Remove(comment);
			_db.SaveChanges();	
		}

		public bool ExistDetail(int? id)
		{
			return _db.Comments_Accommodations.Any(c => c.Id == id);
			
		}

		public Comments_AccommodationsDetailDto Detail(int? id)
		{
			var commAccDb= _db.Comments_Accommodations
				.Include(c => c.Member)
				.Include(c => c.Accommodation)
				.FirstOrDefault(c => c.Id == id);

			var img = _db.Comments_AccommodationImages
				.Where(i => i.Comments_AccommodationId == id)
				.ToList()
				.Select(i => i.Image);

			Comments_AccommodationsDetailDto dto = new Comments_AccommodationsDetailDto
			{
				Id = (int)id,
				MemberAccount = commAccDb.Member.Account,
				AccomodationName = commAccDb.Accommodation.Name,
				Score = commAccDb.Score,
				Title = commAccDb.Title,
				Pros = commAccDb.Pros,
				Cons = commAccDb.Cons,
				Images = img
			};

			return dto;
		}
	}
}