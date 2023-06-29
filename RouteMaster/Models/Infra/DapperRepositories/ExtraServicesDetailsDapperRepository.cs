using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class ExtraServicesDetailsDapperRepository
	{
		private readonly string _connStr;

		public ExtraServicesDetailsDapperRepository()
		{
			_connStr =
			System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		}
		public List<ExtraServicesDetailsVM> GetExtraServicesDetails(int orderId)
		{
			
		string sql = @"SELECT [Id]
      ,[OrderId]
      ,[ExtraServiceId]
      ,[ExtraServiceName]
      ,[Price]
      ,[Quantity]
         FROM ExtraServicesDetails WHERE orderid = @orderid";
			IEnumerable<ExtraServicesDetailsVM> extraServicesDetails = new SqlConnection(_connStr).Query<ExtraServicesDetailsVM>(sql, new { orderid = orderId });
			return extraServicesDetails.ToList();
			}
		}
	}
