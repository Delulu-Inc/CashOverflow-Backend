using CashOverflow.Application.Common.Interfaces;
using CashOverflow.Application.Dtos;
using CashOverflow.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CashOverflow.Infrastructure.Identity
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtService _jwt;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtService jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
        }

      
        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user==null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }
            return _jwt.GenerateToken(user);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto> GetMeAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            if (user==null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var role = roles.FirstOrDefault() ?? "Owner";

            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = role,
                AvatarUrl = user.AvatarUrl
            };

        }
    }
}
