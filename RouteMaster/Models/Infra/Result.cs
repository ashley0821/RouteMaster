using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra
{
	public class Result
	{
		public bool IsSuccess { get; private set; }
		public bool IsFalse => !IsSuccess;

		public string ErrorMessage { get; private set; }

		public static Result Success() => new Result { IsSuccess = true, ErrorMessage = null };

		public static Result Fail(string errormessage) => new Result { IsSuccess = false, ErrorMessage = errormessage };
	}

}