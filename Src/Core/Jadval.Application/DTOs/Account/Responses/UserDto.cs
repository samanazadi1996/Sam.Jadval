using System;

namespace Jadval.Application.DTOs.Account.Responses
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public long Coins { get; set; }
        public long Level { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
