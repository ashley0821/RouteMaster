using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class AttractionService
	{
		private IAttractionRepository _repo;

		public AttractionService(IAttractionRepository repo)
		{
			_repo = repo;
		}

		public IEnumerable<AttractionIndexDto> Search()
		{
			return _repo.Search();
		}

		public AttractionDetailDto Get(int id)
		{
			return _repo.Get(id);
		}

		public IEnumerable<AttractionImageIndexDto> GetImages(int id)
		{
			return _repo.GetImages(id);
		}

		public AttractionEditDto GetEditDto(int id)
		{
			return _repo.GetEditDto(id);
		}

		public Result Create(AttractionCreateDto dto, HttpPostedFileBase[] files, String path)
		{
			// 判斷名稱是否已被用過
			if (_repo.ExistAttraction(dto.Name))
			{
				// 丟出異常，或者回傳 Result
				return Result.Fail($"帳號 {dto.Name} 已存在，請更換後再試一次");
			}

			// 新增一筆紀錄
			_repo.Create(dto, files, path);

			return Result.Success();
		}

		public Result Edit(AttractionEditDto dto)
		{
			_repo.Edit(dto);
			return Result.Success();
		}

		public Result Delete(int id)
		{
			try {
				_repo.Delete(id);

				return Result.Success();
			}
			catch
			{ 
				return Result.Fail("無法刪除"); 
			}
			
		}

		public Result EditImage(AttractionImageIndexDto dto, HttpPostedFileBase file , string path)
		{
			_repo.EditImage(dto, file, path);
			return Result.Success();
		}
	}
}