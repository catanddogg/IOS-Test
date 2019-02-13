using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IllyaVirych.Core.Interface
{
    public interface IWebApiService
    {
         Task<bool> RefreshDataAsync();
    }
}
