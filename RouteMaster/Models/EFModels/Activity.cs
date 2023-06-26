namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Activity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Activity()
        {
            ActivitiesDetails = new HashSet<ActivitiesDetail>();
            Cart_ActivitiesDetails = new HashSet<Cart_ActivitiesDetails>();
            PackageTours = new HashSet<PackageTour>();
            TravelPlans = new HashSet<TravelPlan>();
        }

        public int Id { get; set; }

        public int ActivityCategoryId { get; set; }

        public int AttractionId { get; set; }

        public string Name { get; set; }

        public int RegionId { get; set; }

        public int Price { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }


        public string Description { get; set; }

        public bool Status { get; set; }

        public virtual ActivityCategory ActivityCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivitiesDetail> ActivitiesDetails { get; set; }

        public virtual Attraction Attraction { get; set; }

        public virtual Region Region { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_ActivitiesDetails> Cart_ActivitiesDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageTour> PackageTours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelPlan> TravelPlans { get; set; }
    }
}
