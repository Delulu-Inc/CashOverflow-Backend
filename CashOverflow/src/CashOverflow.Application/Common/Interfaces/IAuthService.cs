using CashOverflow.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task LogoutAsync();

        Task<UserDto> GetMeAsync(string userId);
    }
}
