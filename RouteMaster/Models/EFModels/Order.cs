namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            AccommodationDetails = new HashSet<AccommodationDetail>();
            ActivitiesDetails = new HashSet<ActivitiesDetail>();
            ExtraServicesDetails = new HashSet<ExtraServicesDetail>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int TravelPlanId { get; set; }

        public int PaymentMethodId { get; set; }

        public int PaymentStatus { get; set; }

        public DateTime? CreateDate { get; set; }

        public int Total { get; set; }

        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccommodationDetail> AccommodationDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ActivitiesDetail> ActivitiesDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ExtraServicesDetail> ExtraServicesDetails { get; set; }

        public virtual Member Member { get; set; }

        public virtual PaymentMethod PaymentMethod { get; set; }

        public virtual TravelPlan TravelPlan { get; set; }
    }
}
