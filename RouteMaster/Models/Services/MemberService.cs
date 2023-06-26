using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Services
{
	public class MemberService
	{
		private IMemberRepository _repo;

		public MemberService(IMemberRepository repo)
		{
			_repo = repo;
		}
		public IEnumerable<MemberIndexDto> Seacrh()
		{
			return _repo.Seacrh();
		}
	}
}