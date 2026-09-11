using System;
using System.Collections.Generic;
using System.Text;

namespace CashOverflow.Application.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
