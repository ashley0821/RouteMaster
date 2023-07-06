using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using RouteMaster.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
	public class ExtraServicesDetailsDapperRepository : IExtraServiceDetailsRepository
	{
		private readonly string _connStr;

		public ExtraServicesDetailsDapperRepository()
		{
			_connStr =
			System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ConnectionString;
		}


		public List<ExtraServicesDetailsVM> GetExtraServicesDetails(int orderid)
		{

			string sql = @"SELECT [Id]
		    ,[OrderId]
		    ,[ExtraServiceId]
		    ,[ExtraServiceName]
		    ,[Price]
		    ,[Quantity]
		       FROM ExtraServicesDetails WHERE orderid = @orderid";
			IEnumerable<ExtraServicesDetailsVM> extraServicesDetails = new SqlConnection(_connStr).Query<ExtraServicesDetailsVM>(sql, new { orderid });
			return extraServicesDetails.ToList();
		}

		//		public void Edit(ExtraServicesDetailsDto dto)
		//		{
		//			using (var conn = new SqlConnection(_connStr))
		//			{

		//				string sql = @"Update ExtraServicesDetails SET 
		//[OrderId]=@OrderId, 
		//[ExtraServiceId]=@ExtraServiceId, 
		//[ExtraServiceName]=@ExtraServiceName, 
		//[Price]=@Price, 
		//[Quantity]=@Quantity
		//WHERE Id=@Id";

		//				conn.Execute(sql, dto);
		//			}
		//		}

		


		void IExtraServiceDetailsRepository.ExtraServicesDetailsEdit(ExtraServicesDetailsEditDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"Update ExtraServicesDetails SET 
[OrderId]=@OrderId, 
[ExtraServiceId]=@ExtraServiceId, 
[ExtraServiceName]=@ExtraServiceName, 
[Price]=@Price, 
[Quantity]=@Quantity
WHERE Id=@Id";

				conn.Execute(sql, dto);
				string extraServiceTotalQuery = @"SELECT SUM(Price*Quantity)FROM ExtraServicesDetails where orderid=@orderid";
				int extraServiceTotal = conn.ExecuteScalar<int>(extraServiceTotalQuery, new {orderid = dto.OrderId});

				string sqlOrder = @"UPDATE Orders SET Total = @Total WHERE Id=@OrderId";
				conn.Execute(sqlOrder, new { Total = extraServiceTotal, orderid = dto.OrderId });



			}
		}
		

		ExtraServicesDetailsEditDto IExtraServiceDetailsRepository.GetExtraServicesEditDetails(int id)
		{
			string sql = @"SELECT [Id]
      ,[OrderId]
      ,[ExtraServiceId]
      ,[ExtraServiceName]
      ,[Price]
      ,[Quantity]

  FROM [ExtraServicesDetails]Where id=@id";
			IEnumerable<ExtraServicesDetailsEditDto> extraServicesDetails = new SqlConnection(_connStr).Query<ExtraServicesDetailsEditDto>(sql, new { id });
			return extraServicesDetails.ToList().FirstOrDefault();
		}

		void IExtraServiceDetailsRepository.ExtraServicesDetailsDelete(int id)
		{
			using (var conn= new SqlConnection(_connStr))
			{
					string sql=@"DELETE FROM ExtraServicesDetails WHERE Id=@Id";
				conn.Execute(sql, new { id });
				
			}
		}

		ExtraServicesDetailsVM IExtraServiceDetailsRepository.GetExtraServicesDetailsById(int id)
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"SELECT [Id]
      ,[OrderId]
      ,[ExtraServiceId]
      ,[ExtraServiceName]
      ,[Price]
      ,[Quantity]
  FROM [ExtraServicesDetails]Where id=@id";
				return conn.QuerySingleOrDefault<ExtraServicesDetailsVM>(sql, new { id });
			}
		}

		public IEnumerable<ExtraServicesDetailsDto> Search()
		{
			using (var conn = new SqlConnection(_connStr))
			{

				string sql = @"SELECT [Id]
      ,[OrderId]
      ,[ExtraServiceId]
      ,[ExtraServiceName]
      ,[Price]
      ,[Quantity]
         FROM ExtraServicesDetails";
				return (IEnumerable<ExtraServicesDetailsDto>)conn.Query<ExtraServicesDetail>(sql);
			}
		}

		public void Create(ExtraServicesDetailsDto dto)
		{
			throw new NotImplementedException();
		}
	}

}
