using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IllyaVirych.Core.Services
{
    public class UserService : IUserService
    {
        private SQLiteConnection _sQLiteConnection;

        public UserService(IDatabaseConnectionService sQLiteConnection)
        {
            _sQLiteConnection = sQLiteConnection.GetDatebaseConnection();
            _sQLiteConnection.CreateTable<User>();
        }

        public List<User> GetAllUsers()
        {
            return (from data in _sQLiteConnection.Table<User>() select data).ToList();
        }

        public User GetUser(string currentInstagramUserId)
        {
            return _sQLiteConnection.Table<User>().FirstOrDefault(x => x.UserId == currentInstagramUserId);
        }

        public void InsertUser(User user)
        {
            _sQLiteConnection.Insert(user);
        }
    }
}
