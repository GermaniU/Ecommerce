﻿using Ecommerce.Domain;

namespace Ecommerce.Application.Contracts.Identity
{
    public interface IAuthService
    {
        string GetSessionUser();

        string createToken(User user, IList<string>? roles);
    }
}
