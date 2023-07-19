using System;
using Microsoft.AspNetCore.Identity;

namespace Jadval.Infrastructure.Identity.Models
{

    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole(string name) : base(name)
        {
        }
    }
}