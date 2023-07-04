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

			_db.Comments_Accommodations.Add(commAccDb);

			Comments_AttractionImages img = new Comments_AttractionImages();

			if(file1.Length > 0)
			{
				foreach(var i in file1)
				{
					if (i != null)
					{
						string fileName = SaveUploadedFile(path, i);
						img.Image = fileName;
						_db.Comments_AttractionImages.Add(img);
						_db.SaveChanges();
					}
					else
					{
						_db.SaveChanges();
					}
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
	}
}