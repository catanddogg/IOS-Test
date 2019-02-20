using Android.App;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using IllyaVirych.Core.Services;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platforms.Android.Binding.BindingContext;

namespace IllyaVirych.Droid.Views.Holder
{    
    public class TasksViewHolder : MvxRecyclerViewHolder
    {
        public TextView NameTaskHolder { get; set; }
        public LinearLayout LinearLayoutTaskHolder { get; set; }

        public TasksViewHolder(View itemView, IMvxAndroidBindingContext context) : base(itemView, context)
        {            
            NameTaskHolder = itemView.FindViewById<TextView>(Resource.Id.txt_name);
            LinearLayoutTaskHolder = itemView.FindViewById<LinearLayout>(Resource.Id.layout_main);            
            Typeface tf = Typeface.CreateFromAsset(Application.Context.Assets, Application.Context.Resources.GetString(Resource.String.fontname));
            NameTaskHolder.SetTypeface(tf, TypefaceStyle.Normal);
            this.DelayBind(() =>
            {
                var set = this.CreateBindingSet<TasksViewHolder, TaskItem>();
                set.Bind(this.NameTaskHolder).To(x => x.NameTask);                
                set.Apply();
            });
        }
    }
}