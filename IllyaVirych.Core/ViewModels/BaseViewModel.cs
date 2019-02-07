using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace IllyaVirych.Core.ViewModels
{
    public abstract class BaseViewModel: MvxViewModel
    {
        protected BaseViewModel()
        {
        }        
    }
    public abstract class BaseViewModel <TParameter> : MvxViewModel <TParameter>
        where TParameter : class
        
    {
        protected BaseViewModel()
        {
        }
    }
}
