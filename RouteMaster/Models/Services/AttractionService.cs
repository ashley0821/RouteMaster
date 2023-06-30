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

		public Result Create(AttractionCreateDto dto)
		{
			// 判斷帳號是否已被用過
			if (_repo.ExistAttraction(dto.Name))
			{
				// 丟出異常，或者回傳 Result
				return Result.Fail($"帳號 {dto.Name} 已存在，請更換後再試一次");
			}

			// 新增一筆紀錄
			_repo.Create(dto);

			return Result.Success();
		}
	}
}