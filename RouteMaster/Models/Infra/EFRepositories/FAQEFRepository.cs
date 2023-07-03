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
	public class FAQEFRepository : IFAQRepository
	{
		private AppDbContext _db = new AppDbContext();

		public IEnumerable<FAQIndexDto> Search(FAQCriteria criteria)
		{
			var query = _db.FAQs.Include(q => q.FAQCategory);
			if (criteria.CategoryId != null && criteria.CategoryId.Value > 0)
			{
				query = query.Where(q => q.CategoryId == criteria.CategoryId.Value);
			}
			if (string.IsNullOrEmpty(criteria.Answer) == false)
			{
				query = query.Where(q => q.Answer.Contains(criteria.Answer));
			}
			if (criteria.SortId != null)
			{
				switch (criteria.SortId)
				{
					case 0:
						query = query.OrderBy(q => q.Id);
						break;
					case 1:
						query = query.OrderByDescending(q => q.Helpful);
						break;
					case 2:
						query = query.OrderByDescending(q => q.CreateDate);
						break;
					case 3:
						query = query.OrderByDescending(q => q.ModifiedDate);
						break;
				}
			}

			return query.AsNoTracking()
				.ToList()
				.Select(q => q.ToIndexDto());



		}

		public bool ExistFAQText(FAQCreateDto dto)
		{
			return _db.FAQs.Any(i => i.Question == dto.Question);
		}

		public void Create(FAQCreateDto dto, HttpPostedFileBase[] file1, string path)
		{
			FAQ text = dto.ToCreateEntity();
			_db.FAQs.Add(text);

			FAQImage img = new FAQImage();
			if(file1.Length > 0)
			{
				foreach(var i in file1)
				{
					if (i != null)
					{
						string fileName =SaveUploadedFile(path, i);
						img.Image = fileName;
						_db.FAQImages.Add(img);
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

		public void Update(FAQEditDto dto)
		{
			var questionDb= _db.FAQs.Find(dto.Id);

			questionDb.CategoryId = dto.CategoryId;
			questionDb.Question=dto.Question;
			questionDb.Answer=dto.Answer;
			questionDb.Helpful=dto.Helpful;
			questionDb.ModifiedDate = DateTime.Now;

			_db.SaveChanges();
		}
	}
}