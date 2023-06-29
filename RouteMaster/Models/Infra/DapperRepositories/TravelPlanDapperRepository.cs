using Dapper;
using RouteMaster.Models.Dto;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RouteMaster.Models.Infra.DapperRepositories
{
    public class TravelPlanDapperRepository : ITravelPlanRepository
    {
        private string _connstr;

        public TravelPlanDapperRepository()
        {
            _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppDbContext"].ToString();
        }



        public IEnumerable<TravelPlanIndexDto> Search()
        {
           using(var conn=new SqlConnection(_connstr))
            {
                string sql = @"";

                return conn.Query<TravelPlanIndexDto>(sql); 
            }
        }



        public void Create(TravelPlanCreateDto dto)
        {
           using(var conn =new SqlConnection(_connstr))
            {
                string sql = @"";

                conn.Execute(sql, dto);
            }
        }

        public void Delete(int id)
        {
          
            using(var connn=new SqlConnection(_connstr))
            {
                string sql = @"DELETE FROＭ　TravelPlans WHERE　Id =@Id";
                connn.Execute(sql, id);
            }
        }

        public void Edit(TravelPlanEditDto dto)
        {
            using(var conn=new SqlConnection(_connstr))
            {
                string sql = @"";
                conn.Execute(sql);
            }
        }

     
    }
}