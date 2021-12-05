using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DronnI_Back.Models.RequestModels
{
    public class UserModel
    {
        public string Login { get; set; }//question about "its infotmation which we post 1 time or everyone?"
        public string Password { get; set; }
        public string Email { get; set; }

    }
}
