using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace RouteMaster.Models.Services
{
	public class ActivityService
	{
		private IActivityRepository _repo;

        public ActivityService(IActivityRepository repo)
        {
            _repo = repo;            
        }

        public IEnumerable<ActivityIndexDto> Search()
        {
            return _repo.Search();
        }

        public Result Create(ActivityCreateDto dto)
        {
            //判斷活動是否已存在
            if(_repo.ExistAcativity(dto.Name,dto.AttractionId,dto.StartTime,dto.EndTime))
            {
                return Result.Fail($"活動{dto.Name}在此地點之相同時間段已存在，請確認設定是否有誤");
            }
            _repo.Create(dto);
            return Result.Success();
        }
    }
}