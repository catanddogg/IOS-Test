using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using UIKit;

namespace IllyaVirych.IOS
{
    public  class TaskListCollectionViewSource : MvxCollectionViewSource
    {
        public TaskListCollectionViewSource(UICollectionView collectionView,
                                       NSString defaultCellIdentifier)
            : base(collectionView, defaultCellIdentifier)
        {
        }
    }
}