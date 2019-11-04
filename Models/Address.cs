using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStore.Models
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public bool IsShippingAddress { get; set; }
        [Required]
        public bool IsBillingAddress { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}