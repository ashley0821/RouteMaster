using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Infra;
using RouteMaster.Models.Infra.Criterias;
using RouteMaster.Models.Infra.Extensions;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
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

        public IEnumerable<ActivityIndexDto> Search(ActivityIndexCriteria criteria)
        {
            return _repo.Search(criteria);
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
        public Result Edit(ActivityEditDto dto)
        {
            //todo 邏輯判斷
			  
            _repo.Edit(dto);
            return Result.Success();
        }

        public Result Delete(int id)
        {
            //todo 邏輯判斷

            _repo.Delete(id);
            return Result.Success();
        }


        public Activity GetActivityById(int id)
        {
            return _repo.GetActivityById(id);
            
        }

    }
}