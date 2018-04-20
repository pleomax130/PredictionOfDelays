using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PredictionOfDelays.Core;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class EventDto
    {
        public int EventId { get; set; }
        public ApplicationUserDto Owner { get; set; }
        [Required]
        public LocalizationDto Localization { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of event")]
        [FutureDate(ErrorMessage = "Enter future date")]
        public DateTime EventDate { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
        public int AmountOfMembers { get; set; }
    }
}