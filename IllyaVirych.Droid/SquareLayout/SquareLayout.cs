using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace IllyaVirych.Droid.SquareLayout
{
    public class SquareLayout : LinearLayout
    {
        public SquareLayout(Context context)
            : base(context)
        {

        }
        public SquareLayout(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

        }

        public SquareLayout(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs, defStyle)
        {

        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            base.OnMeasure(widthMeasureSpec, widthMeasureSpec);
        }
    }
}