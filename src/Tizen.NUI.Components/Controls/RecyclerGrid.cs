
using System;
using Tizen.NUI.BaseComponents;

using System.Collections.Generic;
using System.ComponentModel;

namespace Tizen.NUI.Components
{
    public class RecyclerGrid : ScrollableBase
    {
        private ListAdapter mAdapter;
        private View mContainer;
        private Size mListItemSize;

        public class ListItem : Control
        {
            public ListItem()
            {
                
            }
        }

        public class ListAdapter
        {
            private List<object> mData = new List<object>();
            public ListAdapter()
            {
            }

            public virtual ListItem CreateListItem()
            {
                return new ListItem();
            }

            public virtual void BindData(ListItem item, int index)
            {

            }

            public void Notify()
            {
                OnDataChanged?.Invoke(this, new EventArgs());
            }

            public event EventHandler<EventArgs> OnDataChanged;

            public List<object> Data{
                get
                {
                    return mData;
                }
                set
                {
                    mData = value;
                    Notify();
                }
            }
            
        }

        public RecyclerGrid()
        {
            Name = "[RecyclerList]";
            ScrollingDirection = Direction.Horizontal;
            mContainer = new View()
            {
                WidthSpecification = ScrollingDirection == Direction.Vertical? LayoutParamPolicies.MatchParent:LayoutParamPolicies.WrapContent,
                HeightSpecification = ScrollingDirection == Direction.Horizontal? LayoutParamPolicies.MatchParent:LayoutParamPolicies.WrapContent,
                //Temporary, for testing
                Layout = new GridLayout()
                {
                    Rows = 9,
                    LinearOrientation = GridLayout.Orientation.Horizontal,
                    //Columns = 5,
                    //LinearOrientation = GridLayout.Orientation.Vertical,
                },
                Name = "Container"
            };
            Add(mContainer);
            ScrollEvent += OnScroll;
        }

        public ListAdapter Adapter{
            get
            {
                return mAdapter;
            }
            set
            {
                mAdapter = value;
                InitializeChild();
            }
        }

        public new LayoutItem Layout{
            get
            {
                LayoutItem result = base.Layout;
                if(mContainer)
                {
                    result = mContainer.Layout;
                }

                return result;
            }
            set
            {
                if(mContainer)
                {
                    mContainer.Layout = value;
                }
            }
        }

        private int mSpareItemCount = 0;
        private int mTotalItemCount = 0;
        private int mFristItemDataIndex = 0;
 
        private void InitializeChild()
        {
            mListItemSize = mAdapter.CreateListItem().Size;

            GridLayout gridLayout = mContainer.Layout as GridLayout;
            int itemLineCount = (gridLayout.LinearOrientation == GridLayout.Orientation.Vertical ) ? gridLayout.Columns : gridLayout.Rows;

            if(ScrollingDirection == Direction.Horizontal)
            {
                mContainer.WidthSpecification = (int)(mListItemSize.Width * (mAdapter.Data.Count / itemLineCount));
            }
            else
            {
                mContainer.HeightSpecification = (int)(mListItemSize.Height * (mAdapter.Data.Count / itemLineCount));
            }

            //spare = itemLineCount or itemLinecout * 2
            mSpareItemCount = itemLineCount; 

            mTotalItemCount = CalculateTotalItemCount(itemLineCount);

            for(int i = 0; i< mTotalItemCount && i < mAdapter.Data.Count; i++)
            {
                ListItem item = mAdapter.CreateListItem();
                item.Name ="["+i+"] recycle";
                mContainer.Add(item);

                mAdapter.BindData(item,i);
            }
        }

        private int CalculateTotalItemCount(int lineCount)
        {
            int visibleItemCount = 0;
            
            if(ScrollingDirection == Direction.Horizontal)
            {
                visibleItemCount = (int)(WidthSpecification/mListItemSize.Width);
            }
            else
            {
                visibleItemCount = (int)(HeightSpecification/mListItemSize.Height);
            }
            visibleItemCount *= lineCount;
            
            return visibleItemCount + (mSpareItemCount * 2);
        }

        private void OnScroll(object source, ScrollableBase.ScrollEventArgs args)
        {
            LayoutGroup containerLayout = mContainer.Layout as LayoutGroup;

            int newFristItemDataIndex = containerLayout.RecycleItemByCurrentPosition(args.Position, mSpareItemCount);

            if(mFristItemDataIndex != newFristItemDataIndex)
            {
                mFristItemDataIndex = newFristItemDataIndex;
                BindData(newFristItemDataIndex);
            }
        }

        private void BindData(int firstItemDataIndex)
        {
            for(int i = firstItemDataIndex; i < (firstItemDataIndex+mTotalItemCount) && i < mAdapter.Data.Count; i++)
            {
                mAdapter.BindData(mContainer.Children[i-firstItemDataIndex] as ListItem,i);
            }
        }
        
    }
}