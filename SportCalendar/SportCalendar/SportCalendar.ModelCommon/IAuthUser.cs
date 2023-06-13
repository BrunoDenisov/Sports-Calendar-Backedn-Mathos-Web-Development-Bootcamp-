using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.ModelCommon
{
    public interface IAuthUser
    {
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string Role { get; set; }
    }
}
