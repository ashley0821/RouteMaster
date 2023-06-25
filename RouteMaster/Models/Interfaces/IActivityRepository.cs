using RouteMaster.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IActivityRepository
	{
		IEnumerable<ActivityIndexDto> Search();

		void Create(ActivityCreateDto dto);


		//判斷是否已存在該活動，名稱、梯次(時間)、舉辦景點皆相同
		bool ExistAcativity(string activityName,int attractionId,DateTime startTime,DateTime endTime);   


		void Edit(ActivityEditDto dto);

		void Delete(int id);
	}
}
