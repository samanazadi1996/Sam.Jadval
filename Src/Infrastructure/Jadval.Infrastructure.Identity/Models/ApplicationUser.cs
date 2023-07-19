using Microsoft.AspNetCore.Identity;
using System;

namespace Jadval.Infrastructure.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Created = DateTime.UtcNow;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }

    }
}
