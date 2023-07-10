using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class FAQService
	{
		private IFAQRepository _repo;

		public FAQService(IFAQRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<FAQIndexDto> Search(FAQCriteria criteria)
		{
			return _repo.Search(criteria);
		}

		public Result Create(FAQCreateDto dto, HttpPostedFileBase[] file1, string path)
		{
			if (_repo.ExistFAQText(dto))
			{
				return Result.Fail("此項常見問題已存在，請重新輸入新的常見問題");
			}
			_repo.Create(dto, file1, path);
			return Result.Success();
		}

		public Result Update(FAQEditDto dto)
		{
			_repo.Update(dto);
			return Result.Success();
		}

		public void DeleteFAQ(int id)
		{
			if (_repo.ExistImgWithinFAQ(id))
			{
				_repo.ClearImg(id);
			}

			_repo.DeleteFAQ(id);
		}

		public bool ExistDetail(int? id)
		{
			return _repo.ExistDetail(id);
		}
		public FAQDetailDto GetDetail(int? id)
		{
			return _repo.Detail(id);

		}

		public IEnumerable<FAQEditImgIndexDto> GetImgIndex(int? id)
		{
			return _repo.GetImgIndex(id);
		}





	}
}