using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra
{
	public static class OrderHelper
	{
		public static int GetActivitiesTotal(SqlConnection conn, int orderId)
		{
			string activitiesTotalQuery = @"SELECT SUM(Price * Quantity) FROM ActivitiesDetails WHERE orderid = @orderid";
			int activitiesTotal = conn.ExecuteScalar<int>(activitiesTotalQuery, new { orderid = orderId });
			return activitiesTotal;
		}

		public static int GetExtraServiceTotal(SqlConnection conn, int orderId)
		{
			string extraServiceTotalQuery = @"SELECT SUM(Price * Quantity) FROM ExtraServicesDetails WHERE orderid = @orderid";
			int extraServiceTotal = conn.ExecuteScalar<int>(extraServiceTotalQuery, new { orderid = orderId });
			return extraServiceTotal;
		}


	}
}