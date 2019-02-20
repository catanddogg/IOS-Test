using Android.Content;
using Android.Util;
using Android.Widget;

namespace IllyaVirych.Droid.CustomComponent
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