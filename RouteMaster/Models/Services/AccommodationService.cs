using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class AccommodationService
	{
		private readonly IAccommodationRepository _repo;

		public AccommodationService(IAccommodationRepository repo)
		{
			this._repo = repo;
		}

		public IEnumerable<AccommodationIndexDto> Search()
		{
			return _repo.Search();
		}

        public Result Create(AccommodationCreateDto dto)
        {
            if (_repo.ExistAccount(dto.Name))
            {
                //丟出異常,或者傳回 Result
                return Result.Fail($"住宿名稱{dto.Name}已存在, 請確認後再試一次");
            }

            // 新增一筆紀錄
            _repo.Create(dto);

            return Result.Success();
        }
    }
}