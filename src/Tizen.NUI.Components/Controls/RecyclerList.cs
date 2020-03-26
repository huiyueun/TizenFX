
using System;
using Tizen.NUI.BaseComponents;

using System.Collections.Generic;
using System.ComponentModel;

namespace Tizen.NUI.Components
{
    /// <summary>
    /// [Draft] This class provides a View that can scroll a single View with a layout. This View can be a nest of Views.
    /// </summary>
    /// This may be public opened in tizen_6.0 after ACR done. Before ACR, need to be hidden as inhouse API.
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class RecyclerList : ScrollableBase
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

        public RecyclerList()
        {
            Name = "[RecyclerList]";
            mContainer = new View()
            {
                WidthSpecification = ScrollingDirection == Direction.Vertical? LayoutParamPolicies.MatchParent:LayoutParamPolicies.WrapContent,
                HeightSpecification = ScrollingDirection == Direction.Horizontal? LayoutParamPolicies.MatchParent:LayoutParamPolicies.WrapContent,
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                },
                Name="Container"
            };
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

        private int mSpareItemCount = 3;
        private int mTotalItemCount = 0;
        private int mFristItemDataIndex = 0;
 
        private void InitializeChild()
        {
            mListItemSize = mAdapter.CreateListItem().Size;
            mContainer.HeightSpecification = (int)(mListItemSize.Height * mAdapter.Data.Count);
            mTotalItemCount = CalculateTotalItemCount();
            Add(mContainer);

            for(int i = 0; i< mTotalItemCount && i < mAdapter.Data.Count; i++)
            {
                ListItem item = mAdapter.CreateListItem();
                item.Name ="["+i+"] recycle";
                TextLabel label = item.Children[0] as TextLabel;
                label.Text = "["+i+"] recycle";
                mContainer.Add(item);
                mAdapter.BindData(item,i);
            }
        }

        private int CalculateTotalItemCount()
        {
            int visibleItemCount = 0;

            if(ScrollingDirection == Direction.Horizontal)
            {
                visibleItemCount = (int)(Size.Width/mListItemSize.Width);
            }
            else
            {
                visibleItemCount = (int)(Size.Height/mListItemSize.Height);
            }
            
            return visibleItemCount + mSpareItemCount*2;
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
            for(int i = firstItemDataIndex; i<(firstItemDataIndex+mTotalItemCount) && i<mAdapter.Data.Count; i++)
            {
                mAdapter.BindData(mContainer.Children[i-firstItemDataIndex] as ListItem,i);
            }
        }
    }
}