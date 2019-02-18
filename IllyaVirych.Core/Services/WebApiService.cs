using IllyaVirych.Core.Interface;
using IllyaVirych.Core.Models;
using MvvmCross.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.Services
{
    public class WebApiService : IWebApiService
    {
        private ITaskService _iTaskService;        
        HttpClient client;
        public Action OnWebApiSaveHandler { get; set; }       

        public WebApiService(ITaskService iTaskService)
        {
            client = new HttpClient();
            _iTaskService = iTaskService;            
        }   

        public async Task <bool> RefreshTasksAsync()
        {
             var uri = new Uri(string.Format("http://10.10.3.199:65015/api/task/" + CurrentInstagramUser.CurrentInstagramUserId));
             var response = await  client.GetAsync(uri);
             if (response.IsSuccessStatusCode)
             {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<TaskItem>>(content);
                _iTaskService.DeleteAllUserTask(CurrentInstagramUser.CurrentInstagramUserId);
                _iTaskService.InsertAllUserTasks(Items);
                OnWebApiSaveHandler();
            }
             return response.IsSuccessStatusCode;
        }

        public async Task SaveTaskItem(TaskItem item, int id)
        {
            var uri = new Uri(string.Format("http://10.10.3.199:65015/api/task/" + id));
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if(item.Id == 0)
            {
                response = await client.PostAsync(uri, content);
            }
            if(item.Id != 0)
            {
                response = await client.PutAsync(uri, content);
            }
            if(response.IsSuccessStatusCode)
            {
            }
        }

        public async Task DeleteTaskItem(int id)
        {
            var uri = new Uri(string.Format("http://10.10.3.199:65015/api/task/"+id));
            var response = await client.DeleteAsync(uri);
            if(response.IsSuccessStatusCode)
            {
            }
        }
    }
}
