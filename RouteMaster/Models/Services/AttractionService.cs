using RouteMaster.Models.Dto;
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
	}
}