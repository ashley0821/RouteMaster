namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Accommodation
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Accommodation()
        {
            AccommodationDetails = new HashSet<AccommodationDetail>();
            AccommodationImages = new HashSet<AccommodationImage>();
            Cart_AccommodationDetails = new HashSet<Cart_AccommodationDetails>();
            Comments_Accommodations = new HashSet<Comments_Accommodations>();
            Rooms = new HashSet<Room>();
            ServiceInfos = new HashSet<ServiceInfo>();
        }

        public int Id { get; set; }

        public int PartnerId { get; set; }

        [Required]
        [StringLength(850)]
        public string Name { get; set; }

        public string Description { get; set; }

        public double? Grade { get; set; }

        public int RegionId { get; set; }

        public int TownId { get; set; }

        [Required]
        public string Address { get; set; }

        public double? PositionX { get; set; }

        public double? PositionY { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        [StringLength(255)]
        public string IndustryEmail { get; set; }

        public int? ParkingSpace { get; set; }

        public DateTime CreateDate { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccommodationDetail> AccommodationDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccommodationImage> AccommodationImages { get; set; }

        public virtual Partner Partner { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart_AccommodationDetails> Cart_AccommodationDetails { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments_Accommodations> Comments_Accommodations { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Rooms { get; set; }

        public virtual Region Region { get; set; }

        public virtual Town Town { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceInfo> ServiceInfos { get; set; }
    }
}
