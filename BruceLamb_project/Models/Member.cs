﻿using System.ComponentModel.DataAnnotations;

namespace BruceLamb_project.Models
{
    public class Member
    {
        [Key]
        public int BookingId { get; set; }

        public string? FacilityDescription { get; set; }

        public string? BookingDateFrom { get; set; }

        public string? BookingDateTo { get; set; }

        public string? BookedBy { get; set; }

        public string? BookingStatus { get; set; }
        public int? Id { get; internal set; }
    }
}
