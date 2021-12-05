﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DronnI_Back.Models.DbModels;

namespace DronnI_Back.Models.ResponseModels
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }

        public AuthResponse(User user, string token)
        {
            Id = user.Id;
            Login = user.Login;
            Role = user.Role;
            Email = user.Email;
            Token = token;
        }
    }
}
