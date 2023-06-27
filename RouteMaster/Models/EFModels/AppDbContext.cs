using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace RouteMaster.Models.EFModels
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("name=AppDbContext")
        {
        }

        public virtual DbSet<AccommodationDetail> AccommodationDetails { get; set; }
        public virtual DbSet<AccommodationImage> AccommodationImages { get; set; }
        public virtual DbSet<Accommodation> Accommodations { get; set; }
        public virtual DbSet<Activity> Activities { get; set; }
        public virtual DbSet<ActivitiesDetail> ActivitiesDetails { get; set; }
        public virtual DbSet<ActivityCategory> ActivityCategories { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
        public virtual DbSet<AttractionCategory> AttractionCategories { get; set; }
        public virtual DbSet<AttractionImage> AttractionImages { get; set; }
        public virtual DbSet<Attraction> Attractions { get; set; }
        public virtual DbSet<AttractionTag> AttractionTags { get; set; }
        public virtual DbSet<Cart_AccommodationDetails> Cart_AccommodationDetails { get; set; }
        public virtual DbSet<Cart_ActivitiesDetails> Cart_ActivitiesDetails { get; set; }
        public virtual DbSet<Cart_ExtraServicesDetails> Cart_ExtraServicesDetails { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Comments_AccommodationImages> Comments_AccommodationImages { get; set; }
        public virtual DbSet<Comments_Accommodations> Comments_Accommodations { get; set; }
        public virtual DbSet<Comments_AttractionImages> Comments_AttractionImages { get; set; }
        public virtual DbSet<Comments_Attractions> Comments_Attractions { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<ExtraService> ExtraServices { get; set; }
        public virtual DbSet<ExtraServicesDetail> ExtraServicesDetails { get; set; }
        public virtual DbSet<FAQCategory> FAQCategories { get; set; }
        public virtual DbSet<FAQImage> FAQImages { get; set; }
        public virtual DbSet<FAQ> FAQs { get; set; }
        public virtual DbSet<MemberImage> MemberImages { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PackageTour> PackageTours { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<RoomImage> RoomImages { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<ServiceInfo> ServiceInfos { get; set; }
        public virtual DbSet<SpecificRoomPrice> SpecificRoomPrices { get; set; }
        public virtual DbSet<SystemImage> SystemImages { get; set; }
        public virtual DbSet<Town> Towns { get; set; }
        public virtual DbSet<Transportation> Transportations { get; set; }
        public virtual DbSet<TravelPlan> TravelPlans { get; set; }
        public virtual DbSet<Comment_Accommodation_Replies> Comment_Accommodation_Replies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.AccommodationDetails)
                .WithRequired(e => e.Accommodation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.AccommodationImages)
                .WithRequired(e => e.Accommodation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.Cart_AccommodationDetails)
                .WithRequired(e => e.Accommodation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.Comments_Accommodations)
                .WithRequired(e => e.Accommodation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.Rooms)
                .WithRequired(e => e.Accommodation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Accommodation>()
                .HasMany(e => e.ServiceInfos)
                .WithMany(e => e.Accommodations)
                .Map(m => m.ToTable("ServiceInfos_Accommodations").MapLeftKey("AccommodationId").MapRightKey("ServiceInfoId"));

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.ActivitiesDetails)
                .WithRequired(e => e.Activity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Cart_ActivitiesDetails)
                .WithRequired(e => e.Activity)
                .HasForeignKey(e => e.ActivitiesId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.PackageTours)
                .WithMany(e => e.Activities)
                .Map(m => m.ToTable("PackageActivities").MapLeftKey("ActivityId").MapRightKey("PackageTourId"));

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.TravelPlans)
                .WithMany(e => e.Activities)
                .Map(m => m.ToTable("PlanActivities").MapLeftKey("ActivityId").MapRightKey("TravelPlanId"));

            modelBuilder.Entity<ActivityCategory>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.ActivityCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AttractionCategory>()
                .HasMany(e => e.Attractions)
                .WithRequired(e => e.AttractionCategory)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.Attraction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.AttractionImages)
                .WithRequired(e => e.Attraction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.Comments_Attractions)
                .WithRequired(e => e.Attraction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.ExtraServices)
                .WithRequired(e => e.Attraction)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.PackageTours)
                .WithMany(e => e.Attractions)
                .Map(m => m.ToTable("PackageAttractions").MapLeftKey("AttractionId").MapRightKey("PackageTourId"));

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.TravelPlans)
                .WithMany(e => e.Attractions)
                .Map(m => m.ToTable("PlanAttractions").MapLeftKey("AttractionId").MapRightKey("TravelPlanId"));

            modelBuilder.Entity<Attraction>()
                .HasMany(e => e.AttractionTags)
                .WithMany(e => e.Attractions)
                .Map(m => m.ToTable("Tags_Attractions").MapLeftKey("AttractionId").MapRightKey("TagId"));

            modelBuilder.Entity<Comments_Accommodations>()
                .HasMany(e => e.Comments_AccommodationImages)
                .WithRequired(e => e.Comments_Accommodations)
                .HasForeignKey(e => e.Comments_AccommodationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comments_Accommodations>()
                .HasMany(e => e.Comment_Accommodation_Replies)
                .WithRequired(e => e.Comments_Accommodations)
                .HasForeignKey(e => e.Comments_AccommodationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comments_Accommodations>()
                .HasMany(e => e.Members)
                .WithMany(e => e.Comments_Accommodations1)
                .Map(m => m.ToTable("Comment_Accommodation_Likes").MapLeftKey("Comments_AccommodationId").MapRightKey("MemberId"));

            modelBuilder.Entity<Comments_Attractions>()
                .HasMany(e => e.Comments_AttractionImages)
                .WithRequired(e => e.Comments_Attractions)
                .HasForeignKey(e => e.Comments_AttractionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExtraService>()
                .HasMany(e => e.Cart_ExtraServicesDetails)
                .WithRequired(e => e.ExtraService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExtraService>()
                .HasMany(e => e.ExtraServicesDetails)
                .WithRequired(e => e.ExtraService)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExtraService>()
                .HasMany(e => e.PackageTours)
                .WithMany(e => e.ExtraServices)
                .Map(m => m.ToTable("PackageExtraServices").MapLeftKey("ExtraServiceId").MapRightKey("PackageTourId"));

            modelBuilder.Entity<ExtraService>()
                .HasMany(e => e.TravelPlans)
                .WithMany(e => e.ExtraServices)
                .Map(m => m.ToTable("PlanExtraServices").MapLeftKey("ExtraServiceId").MapRightKey("TravelPlanId"));

            modelBuilder.Entity<FAQCategory>()
                .HasMany(e => e.FAQs)
                .WithRequired(e => e.FAQCategory)
                .HasForeignKey(e => e.CategoryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FAQ>()
                .HasMany(e => e.FAQImages)
                .WithRequired(e => e.FAQ)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Comments_Accommodations)
                .WithRequired(e => e.Member)
                .HasForeignKey(e => e.MemberId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Comments_Attractions)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.MemberImages)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.TravelPlans)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.Schedules)
                .WithRequired(e => e.Member)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.AccommodationDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.ActivitiesDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.ExtraServicesDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Partner>()
                .HasMany(e => e.Accommodations)
                .WithRequired(e => e.Partner)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PaymentMethod>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.PaymentMethod)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Accommodations)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Activities)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Attractions)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Region>()
                .HasMany(e => e.Towns)
                .WithRequired(e => e.Region)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.RoomImages)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Room>()
                .HasMany(e => e.SpecificRoomPrices)
                .WithRequired(e => e.Room)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SpecificRoomPrice>()
                .Property(e => e.NewPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Town>()
                .HasMany(e => e.Accommodations)
                .WithRequired(e => e.Town)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Town>()
                .HasMany(e => e.Attractions)
                .WithRequired(e => e.Town)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TravelPlan>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.TravelPlan)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TravelPlan>()
                .HasMany(e => e.Transportations)
                .WithRequired(e => e.TravelPlan)
                .WillCascadeOnDelete(false);
        }

        public System.Data.Entity.DbSet<RouteMaster.Models.ViewModels.ActivityIndexVM> ActivityIndexVMs { get; set; }

        public System.Data.Entity.DbSet<RouteMaster.Models.ViewModels.AccommodationCreateVM> AccommodationCreateVMs { get; set; }
    }
}
