using System;
using System.Collections.Generic;
using System.Text;

namespace Jadval.Application.Interfaces
{
    public interface IAuthenticatedUserService
    {
        string UserId { get; }
        string UserName { get; }
    }
}
