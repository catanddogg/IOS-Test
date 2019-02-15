using IllyaVirych.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.Interface
{
    public interface IWebApiService
    {
        Action OnWebApiSaveHandler { get; set; }
        Task<bool> RefreshTasksAsync();
        Task SaveTaskItem(TaskItem item, int id);
        Task DeleteTaskItem(int id);
    }
}
