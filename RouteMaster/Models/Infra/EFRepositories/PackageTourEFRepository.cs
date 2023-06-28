using RouteMaster.Models.Dto;
using RouteMaster.Models.EFModels;
using RouteMaster.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using RouteMaster.Models.Infra.Extensions;

namespace RouteMaster.Models.Infra.EFRepositories
{
    public class PackageTourEFRepository : IPackageTourRepository
    {
        private AppDbContext _db;
        public PackageTourEFRepository()
        {
            _db = new AppDbContext();
        }


        public IEnumerable<PackageTourIndexDto> Search()
        {
            var packageTours = _db.PackageTours.Include(p => p.Coupon).ToList().Select(x => x.ToIndexDto());
            return packageTours;                     
        }


        public void Create(PackageTourCreateDto dto)
        {
            PackageTour packageTour= dto.ToEntity();
            _db.PackageTours.Add(packageTour);
            _db.SaveChanges();

        }

        public void Delete(int id)
        {
            var packageTour = _db.PackageTours.Find(id);
            _db.PackageTours.Remove(packageTour);
            _db.SaveChanges();
        }

        public void Edit(PackageTourEditDto dto)
        {
            var packageInDb = _db.PackageTours.Find(dto.Id);

            packageInDb.Description = dto.Description;
            packageInDb.Status= dto.Status; 
            packageInDb.CouponId = dto.CouponId;



            //todo


            //packageInDb.Activities.Add(dto.Activities)
            //packageInDb.ExtraServices = dto.ExtraServices;

            _db.SaveChanges();


          
          

        }

   
    }
}