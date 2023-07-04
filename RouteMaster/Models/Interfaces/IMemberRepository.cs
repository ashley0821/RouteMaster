using RouteMaster.Models.Dto;
using RouteMaster.Models.Infra.Criterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteMaster.Models.Interfaces
{
	public interface IMemberRepository
	{
		IEnumerable<MemberIndexDto> Seacrh(MemberCriteria criteria);

		void Register(MemberRegisterDto dto);

		bool ExistAccount(string account); // 判斷帳號是否存在

		void UpdateMemberImage(MemberImageCreateDto dto);
	}
}
