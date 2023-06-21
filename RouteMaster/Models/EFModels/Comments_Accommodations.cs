namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments_Accommodations
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comments_Accommodations()
        {
            Comments_AccommodationImages = new HashSet<Comments_AccommodationImages>();
            Comment_Accommodation_Replies = new HashSet<Comment_Accommodation_Replies>();
            Members = new HashSet<Member>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int AccommodationId { get; set; }

        public double Score { get; set; }

        [Required]
        public string Title { get; set; }

        public string Pros { get; set; }

        public string Cons { get; set; }

        public DateTime? CreateDate { get; set; }

        public virtual Accommodation Accommodation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments_AccommodationImages> Comments_AccommodationImages { get; set; }

        public virtual Member Member { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment_Accommodation_Replies> Comment_Accommodation_Replies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }
    }
}
