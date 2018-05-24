using System;
using System.ComponentModel.DataAnnotations;

namespace PredictionOfDelays.Core.Models
{
    public class ExceptionWrapper
    {
        [Key]
        public int Id { get; set; }
        public string ExceptionMessage { get; set; }
        public string ControllerName{ get; set; }
        public string ExceptionStackTrace { get; set; }
        public DateTime LogTime { get; set; }
    }
}