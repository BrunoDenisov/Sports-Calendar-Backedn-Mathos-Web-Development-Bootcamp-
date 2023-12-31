﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportCalendar.ModelCommon
{
    public interface IAuthUser
    {
        Guid Id { get; set; } 
        string Username { get; set; }
        string Password { get; set; }
        string Email { get; set; }
        string Access { get; set; }
    }
}
