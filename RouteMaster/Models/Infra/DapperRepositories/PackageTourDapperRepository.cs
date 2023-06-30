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

    public class PackageTourDapperRepository : IPackageTourRepository
    {
        private string _connstr;

        public PackageTourDapperRepository()
        {
            _connstr = System.Configuration.ConfigurationManager.ConnectionStrings["AppContext"].ToString();
        }


        public IEnumerable<PackageTourIndexDto> Search()
        {
            using(var conn=new SqlConnection(_connstr))
            {
                string sql = @"";
                return conn.Query<PackageTourIndexDto>(sql);    
            }
        }



        public void Create(PackageTourCreateDto dto)
        {
            using (var conn = new SqlConnection(_connstr))
            {
                string sql = @"";
                conn.Execute(sql, dto);
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(_connstr))
            {
                string sql = @"DELETE FROM　PackageTour WHERE Id=@Id";
                conn.Execute(sql, id);
            }
        }

        public void Edit(PackageTourEditDto dto)
        {
            using (var conn = new SqlConnection(_connstr))
            {
                string sql = @"";
                conn.Execute(sql,dto);
            }
        }

    
    }
}