using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public string OwnerUserId { get; set; }
        [Required]
        [MaxLength(50), MinLength(5)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
    }
}