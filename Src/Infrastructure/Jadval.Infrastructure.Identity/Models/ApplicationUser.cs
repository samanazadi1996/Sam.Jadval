﻿using Microsoft.AspNetCore.Identity;
using System;

namespace Jadval.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Created = DateTime.Now;
            Coins = 100;
            Level = 1;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public long Coins { get; set; }
        public long Level { get; set; }

    }
}
