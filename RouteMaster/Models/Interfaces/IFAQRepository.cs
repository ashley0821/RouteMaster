using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra.Criterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RouteMaster.Models.Interfaces
{
	public interface IFAQRepository
	{
		IEnumerable<FAQIndexDto> Search(FAQCriteria criteria);

		bool ExistFAQText(FAQCreateDto dto);
		void Create(FAQCreateDto dto, HttpPostedFileBase[] file1, string path);

		void Update(FAQEditDto dto);

		bool ExistImgWithinFAQ(int id);

		void ClearImg(int id);
		void DeleteFAQ(int id);
	}
}
