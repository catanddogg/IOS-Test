using IllyaVirych.Core.Helper;
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
        private ITaskService _taskService;        
        HttpClient _client;
        private readonly string _wepApiAddressServer = "http://10.10.3.199:65015/api/task/";

        public WebApiService(ITaskService taskService)
        {
            _client = new HttpClient();
            _taskService = taskService;            
        }

        public async Task<List<TaskItem>> RefreshDataAsync()
        {
            List<TaskItem> items = null;            
            var currentUserId = UserInstagramId.UserId();
            var uri = new Uri(string.Format(_wepApiAddressServer + currentUserId));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<TaskItem>>(content);
                _taskService.DeleteAllUserTask(UserInstagramId.UserId());
                _taskService.InsertAllUserTasks(items);
            }
            return items;

        }

        public async Task SaveTaskItem(TaskItem item, int id)
        {
            var uri = new Uri(string.Format(_wepApiAddressServer + id));
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            if(item.Id == 0)
            {
                response = await _client.PostAsync(uri, content);
            }
            if(item.Id != 0)
            {
                response = await _client.PutAsync(uri, content);
            }
            if(response.IsSuccessStatusCode)
            {
            }
        }

        public async Task DeleteTaskItem(int id)
        {
            var uri = new Uri(string.Format(_wepApiAddressServer + id));
            var response = await _client.DeleteAsync(uri);
            if(response.IsSuccessStatusCode)
            {
            }
        }
    }
}
