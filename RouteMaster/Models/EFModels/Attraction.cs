namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Attraction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Attraction()
        {
            Activities = new HashSet<Activity>();
            AttractionImages = new HashSet<AttractionImage>();
            Comments_Attractions = new HashSet<Comments_Attractions>();
            ExtraServices = new HashSet<ExtraService>();
            PackageTours = new HashSet<PackageTour>();
            TravelPlans = new HashSet<TravelPlan>();
            AttractionTags = new HashSet<AttractionTag>();
        }

        public int Id { get; set; }

        public int AttractionCategoryId { get; set; }

        public int RegionId { get; set; }

        public int TownId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Address { get; set; }

        public double? PositionX { get; set; }

        public double? PositionY { get; set; }

        [Required]
        public string Description { get; set; }

        public string Website { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Activity> Activities { get; set; }

        public virtual AttractionCategory AttractionCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttractionImage> AttractionImages { get; set; }

        public virtual Region Region { get; set; }

        public virtual Town Town { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments_Attractions> Comments_Attractions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtraService> ExtraServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageTour> PackageTours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelPlan> TravelPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttractionTag> AttractionTags { get; set; }
    }
}
