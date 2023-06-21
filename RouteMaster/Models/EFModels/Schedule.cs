namespace RouteMaster.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Schedule
    {
        public int Id { get; set; }

        public int MemberId { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CreatDate { get; set; }

        public virtual Member Member { get; set; }
    }
}
