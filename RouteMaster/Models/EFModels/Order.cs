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
		[Display(Name = "訂單編號")]
		public int Id { get; set; }
		[Display(Name = "訂購人編號")]
		public int MemberId { get; set; }
		[Display(Name = "旅行計畫編號")]
		public int TravelPlanId { get; set; }
		[Display(Name = "付款方式代號")]
		public int PaymentMethodId { get; set; }
		[Display(Name = "付款狀態")]
		public int PaymentStatus { get; set; }
		[Display(Name = "訂單成立日期")]
		[DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm}", ApplyFormatInEditMode = true)]
		public DateTime? CreateDate { get; set; }
		[Display(Name = "總金額")]
		[DisplayFormat(DataFormatString = "{0:#,#}")]
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
