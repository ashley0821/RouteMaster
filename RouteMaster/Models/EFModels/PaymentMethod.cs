namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PaymentMethod
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PaymentMethod()
        {
            Orders = new HashSet<Order>();
        }
		[Display(Name = "½s¸¹")]
		public int Id { get; set; }

        [Required]
        [StringLength(100)]
		[Display(Name = "¥I´Ú¤è¦¡")]
		public string Name { get; set; }

        [Required]
		[Display(Name = "´y­z")]
		public string Description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
