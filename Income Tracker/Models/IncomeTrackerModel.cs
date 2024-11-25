using System;
using System.ComponentModel.DataAnnotations;

namespace Income_Tracker.Models
{
    public class IncomeTrackerModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, 263000000000, ErrorMessage = "Amount must be between 0.01 and 263 Billion.")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Amount must have up to 2 decimal places.")]
        public decimal Quantity { get; set; }
    }
}
