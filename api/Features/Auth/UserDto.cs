using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.Features.Auth
{
    public class UserDto
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
