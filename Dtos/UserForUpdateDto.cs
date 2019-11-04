﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineStore.Helpers.Enums;

namespace OnlineStore.Dtos
{
    public class UserForUpdateDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string TelephoneCell { get; set; }
        public string TelephoneHome { get; set; }
        public DateTime DoB { get; set; }
        public string KnownAs { get; set; }
        public string Type { get; set; }
    }
}
