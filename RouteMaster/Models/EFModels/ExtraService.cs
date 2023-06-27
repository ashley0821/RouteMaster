namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ExtraService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ExtraService()
        {
            Cart_ExtraServicesDetails = new HashSet<Cart_ExtraServicesDetails>();
            ExtraServicesDetails = new HashSet<ExtraServicesDetail>();
            PackageTours = new HashSet<PackageTour>();
            TravelPlans = new HashSet<TravelPlan>();
        }

        public int Id { get; set; }

        
        public string Name { get; set; }

        public int AttractionId { get; set; }

        public int Price { get; set; }

     
        public string Description { get; set; }

        public bool Status { get; set; }

        public virtual Attraction Attraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_ExtraServicesDetails> Cart_ExtraServicesDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtraServicesDetail> ExtraServicesDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PackageTour> PackageTours { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TravelPlan> TravelPlans { get; set; }
    }
}
