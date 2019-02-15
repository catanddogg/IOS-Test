using CoreGraphics;
using Foundation;
using IllyaVirych.Core.Services;
using IllyaVirych.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.UI;
using MvvmCross.ViewModels;
using System;
using UIKit;

namespace IllyaVirych.IOS
{
    public partial class ListTaskNameCell : MvxCollectionViewCell
    {
        public static readonly UINib Nib = UINib.FromName("ListTaskNameCell", NSBundle.MainBundle);
        public static readonly NSString Key = new NSString("ListTaskNameCell");        

        public ListTaskNameCell (IntPtr handle) : base (handle)
        {
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<ListTaskNameCell, TaskItem>();
                set.Bind(NameTaskLabel).To(vm => vm.NameTask);
                set.Bind(this).For(v => v.BackgroundColor).To(vm => vm.StatusTask).WithConversion("Color");
                set.Apply();                           
            });           

        }
        public static ListTaskNameCell Create()
        {
            return (ListTaskNameCell)Nib.Instantiate(null, null)[0];
        }
    }
}