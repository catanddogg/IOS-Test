using IllyaVirych.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IllyaVirych.WPF.Services
{
    public class AlertService : IAlertService
    {
        public void ShowAlert(string message)
        {
            MessageBox.Show(message); 
        }
    }
}
