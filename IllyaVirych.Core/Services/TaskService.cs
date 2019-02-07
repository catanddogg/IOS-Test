using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IllyaVirych.Core.Services
{
    public class TaskService : ITaskService
    {
        private SQLiteConnection _sQLiteConnection;

        public TaskService(IDatabaseConnectionService sQLiteConnection)
        {
            _sQLiteConnection = sQLiteConnection.GetDatebaseConnection();
            _sQLiteConnection.CreateTable<TaskItem>();
            _sQLiteConnection.CreateTable<User>();            
        }

        public void DeleteTask(int _idTask)
        {
            _sQLiteConnection.Delete<TaskItem>(_idTask);
        }
        public void InsertTask(TaskItem task)
        {
            if (task.Id == 0)
            {
                _sQLiteConnection.Insert(task);
            }
            if (task.Id != 0)
            {
                _sQLiteConnection.Update(task);
            }
        }
        public List<TaskItem> GetUserTasks(string currentUserId)
        {
            return (from data in _sQLiteConnection.Table<TaskItem>() where data.UserId == currentUserId select data).ToList();
        }

        public List<TaskItem> GetAllTasks()
        {
            return (from data in _sQLiteConnection.Table<TaskItem>() select data).ToList();
        }

        public List<User> GetAllUsers()
        {
            return (from data in _sQLiteConnection.Table<User>() select data).ToList();
        }
        public  User GetUser(string currentInstagramUserId)
        {
            return _sQLiteConnection.Table<User>().FirstOrDefault(x => x.UserId == currentInstagramUserId);
        }
        public void InsertUser(User user)
        {        
                _sQLiteConnection.Insert(user);
        }        
    }
}
