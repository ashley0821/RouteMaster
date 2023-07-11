namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Partner
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Partner()
        {
            Accommodations = new HashSet<Accommodation>();
        }

        public int Id { get; set; }

        [Display(Name = "名")]
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

		[Display(Name = "姓")]
		[Required]
        [StringLength(50)]
        public string LastName { get; set; }

		[Display(Name = "信箱")]
		[Required]
        [StringLength(255)]
        public string Email { get; set; }


        [Required]
        [StringLength(255)]
        public string EncryptedPassword { get; set; }

		[Display(Name = "註冊日期")]
		public DateTime CreateDate { get; set; }

		[Display(Name = "啟用")]
		public bool IsConfirmed { get; set; }


        public string ConfirmCode { get; set; }

		[Display(Name = "停權")]
		public bool? IsSuspended { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Accommodation> Accommodations { get; set; }
    }
}
