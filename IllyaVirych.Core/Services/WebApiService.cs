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
        
        public WebApiService(ITaskService iTaskService)
        {
            client = new HttpClient();
            _iTaskService = iTaskService;            
        }   

        public async Task<bool> RefreshDataAsync()
        {            
            var uri = new Uri(string.Format("http://10.10.3.199:65015/api/item/1"));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var Items = JsonConvert.DeserializeObject<List<TaskItem>>(content);
                //_iTaskService.DeleteTask(CurrentInstagramUser.CurrentInstagramUserId);
            }
            return response.IsSuccessStatusCode;
        }

        public async Task SaveTaskItemAsync(TaskItem item, bool isNewItem = false)
        {
            var uri = new Uri("http://10.10.3.199:65015/api/item");
            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = null;
            if (isNewItem)
            {
                response = await client.PostAsync(uri, content);
            }
            if(response.IsSuccessStatusCode)
            {
                Console.WriteLine(@"Save");
            }
        }

        public async Task DeleteTaskIemAsync(string id)
        {
            var uri = new Uri("http://10.10.3.199:65015");
            var response = await client.DeleteAsync(uri);
        }
        //public async Task<MvxObservableCollection<TaskItem>> GetTaskItemsAsync (bool syncItems = false)
        //{
        //    IEnumerable<TaskItem> items = await _iTaskService
        //        .Where()
        //}
    }
}
