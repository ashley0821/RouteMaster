namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comments_Attractions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comments_Attractions()
        {
            Comments_AttractionImages = new HashSet<Comments_AttractionImages>();
        }

        public int Id { get; set; }

        public int MemberId { get; set; }

        public int AttractionId { get; set; }

        public int Score { get; set; }

        public string Content { get; set; }

        public int? StayHours { get; set; }

        public int? Price { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Attraction Attraction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comments_AttractionImages> Comments_AttractionImages { get; set; }

        public virtual Member Member { get; set; }
    }
}
