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
	public class ActivitiesDetailsDapperRepository:IActivitiesDetailsRepository
	{
		private readonly string _connStr;

		public ActivitiesDetailsDapperRepository()
		{
			_connStr = System.Configuration.ConfigurationManager.ConnectionStrings
				["AppDbContext"].ConnectionString;
		}

		public List<ActivitiesDetailsIndexVM>GetActivitiesDetails(int orderid)
		{
			
			string sql = @"SELECT [id],
       [OrderId]
      ,[ActivityId]
      ,[ActivityName]
      ,[StartTime]
      ,[EndTime]
      ,[Price]
      ,[Quantity]
  FROM ActivitiesDetails where orderid =@orderid ";
			IEnumerable<ActivitiesDetailsIndexVM>activitiesDetails=new SqlConnection(_connStr).Query<ActivitiesDetailsIndexVM>(sql, new { orderid=orderid });
            return activitiesDetails.ToList();
        }

		ActivitiesDetailsEditDto IActivitiesDetailsRepository.GetActivitiesDetailsEditDetails(int id)
		{
			string sql = @"SELECT [Id]
      ,[OrderId]
      ,[ActivityId]
      ,[ActivityName]
      ,[StartTime]
      ,[EndTime],[Price],[Quantity]
 FROM [ActivitiesDetails]Where id=@id";
			IEnumerable<ActivitiesDetailsEditDto>activitiesDetails = new SqlConnection(_connStr).Query<ActivitiesDetailsEditDto>(sql, new { id });
			return activitiesDetails.ToList().FirstOrDefault();
		}


		void IActivitiesDetailsRepository.ActivitiesDetailsDelete(int id)
		{
			using(var conn=new SqlConnection(_connStr))
			{
				string sql = @"DELETE FROM ActivitiesDetails WHERE Id=@Id";
				conn.Execute(sql, new { id });
			}
		}

		ActivitiesDetailsIndexVM IActivitiesDetailsRepository.GetActivitiesDetailsById(int id)
		{
			using(var conn=new SqlConnection(_connStr))
			{
				string sql = "select [Id] ,[OrderId],[ActivityId],[ActivityName],[StartTime],[EndTime],[Price],[Quantity] from [ActivitiesDetails] WHere Id=@id";
				return conn.QuerySingleOrDefault<ActivitiesDetailsIndexVM>(sql, new { id });
			}
		}


		void IActivitiesDetailsRepository.ActivitiesDetailsEdit(ActivitiesDetailsEditDto dto)
		{
			using (var conn = new SqlConnection(_connStr))
			{
				string sql = @"Update ActivitiesDetails SET 
[OrderId]=@OrderId, 
[ActivityId]=@ActivityId, 
[ActivityName]=@ActivityName, 
[StartTime]=@StartTime, 
[EndTime]=@EndTime,
[Price]=@Price,
[Quantity]=@Quantity
WHERE Id=@Id";
				conn.Execute(sql, dto);

				string activitiesTotalQuery = @"SELECT SUM(Price * Quantity) FROM ActivitiesDetails WHERE orderid = @orderid";
				int activitiesTotal = conn.ExecuteScalar<int>(activitiesTotalQuery, new { orderid = dto.OrderId });

				string sqlOrder = @"UPDATE Orders SET Total = @Total WHERE Id = @OrderId";
				conn.Execute(sqlOrder, new { Total = activitiesTotal, orderid = dto.OrderId });
				//conn.Execute(sqlOrder, dto);


			}
		}
		void IActivitiesDetailsRepository.Create(ActivitiesDetailsDto dto)
		{
			throw new NotImplementedException();
		}
		IEnumerable<ActivitiesDetailsDto> IActivitiesDetailsRepository.Search()
		{
			throw new NotImplementedException();
		}

		
	}
}
	