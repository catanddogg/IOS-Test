using System;
using Foundation;
using IllyaVirych.Core.Services;
using MvvmCross.Platforms.Ios.Binding.Views;
using MvvmCross.ViewModels;
using UIKit;

namespace IllyaVirych.IOS
{
    public  class TaskListCollectionViewSource : MvxCollectionViewSource
    {
        //private ListTaskView _listTaskView;
        //private MvxObservableCollection<TaskItem> _items;

        //public TaskListCollectionViewSource(ListTaskView listTaskView, MvxObservableCollection<TaskItem> items)
            
        //{
        //    this._listTaskView = listTaskView;
        //    this._items = items;
        //}

        public TaskListCollectionViewSource(UICollectionView collectionView,
                                       NSString defaultCellIdentifier)
            : base(collectionView, defaultCellIdentifier)
        {
        }

        //public override nint NumberOfSections(UICollectionView collectionView)
        //{
        //    return 1;
        //}

        //public override nint GetItemsCount(UICollectionView collectionView, nint section)
        //{
        //    return _items.Count;
        //}

        //public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        //{
        //    var cell = (ListTaskName)collectionView.DequeueReusableCell(ListTaskName.CellID, indexPath);
        //    cell.UpdateRow(_items, indexPath);
        //    return cell;
        //}
    }
}