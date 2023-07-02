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
            if (_repo.ExistName(dto.Name))
            {
                //丟出異常,或者傳回 Result
                return Result.Fail($"住宿名稱{dto.Name}已存在, 請確認後再試一次");
            }

			if (dto.RegionId == 0 || dto.TownId == 0) return Result.Fail("請再確認欄位資料是否正確");

			// 新增一筆紀錄
			_repo.Create(dto);

            return Result.Success();
        }

		public AccommodationEditDto GetEditInfo(int? id)
		{
			return _repo.GetEditInfo(id);
		}

		public Result EditAccommodationProfile(AccommodationEditDto dto)
		{
			if (!_repo.ExistName(dto.Name) || !_repo.IsOriginalName(dto))
			{
				//丟出異常,或者傳回 Result
				return Result.Fail($"住宿名稱{dto.Name}已存在, 請確認後再試一次");
			}

			if (dto.RegionId == 0 || dto.TownId == 0) return Result.Fail("請再確認欄位資料是否正確");

			// 新增一筆紀錄
			_repo.EditAccommodationProfile(dto);

			return Result.Success();
			
		}

		public Result CreateRoomAndImages(RoomCreateDto dto, HttpPostedFileBase[] files, String path)
		{
			
			// 新增一筆紀錄
			_repo.CreateRoomAndImages(dto, files, path);

			return Result.Success();
		}
	}
}