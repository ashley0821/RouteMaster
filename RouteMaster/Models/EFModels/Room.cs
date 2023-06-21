namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Room
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Room()
        {
            RoomImages = new HashSet<RoomImage>();
            SpecificRoomPrices = new HashSet<SpecificRoomPrice>();
        }

        public int Id { get; set; }

        public int AccommodationId { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

        public virtual Accommodation Accommodation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoomImage> RoomImages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecificRoomPrice> SpecificRoomPrices { get; set; }
    }
}
