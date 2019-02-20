using IllyaVirych.Core.Models;
using IllyaVirych.Core.Services;
using System;
using System.Collections.Generic;

namespace IllyaVirych.Core.Interface
{
    public interface ITaskService
    {
        List<TaskItem> GetAllTasks();
        void DeleteTask(int _idTask);
        void InsertTask(TaskItem task);
        List<TaskItem> GetUserTasks(string currentUserId);
        void DeleteAllUserTask(string curretUserId);
        void InsertAllUserTasks(List<TaskItem> task);           
    }
}
