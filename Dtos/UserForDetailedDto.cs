﻿using OnlineStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string TelephoneCell { get; set; }
        public string TelephoneHome { get; set; }
        public int Age { get; set; }
        public string KnownAs { get; set; }
        public string Type { get; set; }
        public int NumberOfLogons { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastActive { get; set; }
        public ICollection<AddressForListDto> Addresses { get; set; }
        public ICollection<OrderForListDto> Orders { get; set; }
    }
}
