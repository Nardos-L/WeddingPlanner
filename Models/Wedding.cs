using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set; }

        [Display(Name = "Wedder One")]
        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        public string WedderOne { get; set; }

        [Display(Name = "Wedder Two")]
        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least 2 characters")]
        public string WedderTwo { get; set; }

        [FutureDate]
        [Required(ErrorMessage = "is required.")]
        [DataType(DataType.Date)] // Just Date, no Time.
        public DateTime? Date { get; set; }

        [Display(Name = "Wedding Address")]
        [Required(ErrorMessage = "is required.")]
        public string WeddingAddress { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // 1 user : Many Wedding
        public int UserId { get; set; }
        public User Planner { get; set; }

        public List<User> Guests { get; set; }

        //Many User : Many Wedding for Rsvp 
        public List<Rsvp> Attendees { get; set; }

        public class FutureDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value == null)
                {
                    return new ValidationResult("Must enter a date.");
                }
                DateTime today = DateTime.Today;
                if ((DateTime)value <= today)
                {
                    return new ValidationResult("Date must be in the future.");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
        }
    }
}