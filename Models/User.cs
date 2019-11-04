using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string TelephoneCell { get; set; }
        public string TelephoneHome { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public string Type { get; set; }
        public int NumberOfLogons { get; set; }
        public DateTime LastActive { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<UserItem> UserItems { get; set; }
    }
}