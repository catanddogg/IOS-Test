using IllyaVirych.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.Interface
{
    public  interface IUserService
    {
        List<User> GetAllUsers();
        User GetUser(string currentInstagramUserId);
        void InsertUser(User user);
    }
}
