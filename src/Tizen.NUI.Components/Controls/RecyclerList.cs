
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
        private Adaptor mAdaptor;
        private View mContainer;
        private Size mListItemSize;

        public class ListItem : Control
        {
            public ListItem()
            {
                
            }
        }

        public class Adaptor
        {
            private List<object> mData = new List<object>();
            public Adaptor()
            {
            }

            internal ListItem GetListItem()
            {
                return CreateListItem();
            }

            internal void BindDataWithItem(ListItem item, int index)
            {
                BindData(item,index);
            }

            protected virtual ListItem CreateListItem()
            {
                return new ListItem();
            }

            protected virtual void BindData(ListItem item, int index)
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

        public RecyclerList(Adaptor adaptor, LayoutItem layout)
        {
            Name = "[RecyclerList]";
            mAdaptor = adaptor;
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
            Add(mContainer);

            ScrollEvent += OnScroll;
            InitializeChild();
        }

        public int SpareChildren{get;set;} = 5;
        private int mFirstBindedIndex = 0;

        private void InitializeChild()
        {
            mFirstBindedIndex = 0;

            Color[] colorList = {
                Color.Red,
                Color.Yellow,
                Color.Green,
                Color.Blue,
                Color.Magenta,
            };

            for(int i = 0 ; i< SpareChildren ; i++)
            {
                ListItem item = mAdaptor.GetListItem();
                item.Name ="["+i+"] recycle";
                mAdaptor.BindDataWithItem(item,i);

                item.BackgroundColor = colorList[i];
                item.Position = new Position(0,item.CurrentSize.Height*i);
                mContainer.Add(item);

                if( mListItemSize == null)
                {
                    mListItemSize = item.Size;
                }
            }

            mContainer.HeightSpecification = (int)(mListItemSize.Height * mAdaptor.Data.Count);
        }

        public void OnScroll(object source, ScrollableBase.ScrollEventArgs args)
        {
            RecycleItemByCurrentPosition(args.Position);
        }

        private void RecycleItemByCurrentPosition(Position position)
        {
            int candidateStart =(int)(-position.Y /  mListItemSize.Height);

            if(mFirstBindedIndex<candidateStart)
            {
                // remove front and move it to tail
                ListItem target = mContainer.Children[mFirstBindedIndex%5] as ListItem;

                int targetIndex = mFirstBindedIndex+5;
                mAdaptor.BindDataWithItem(target,targetIndex);
                target.Position = new Position(0,(int)((targetIndex)*mListItemSize.Height));

            }
            else if(mFirstBindedIndex>candidateStart)
            {
                // remove tail and move it to fornt
                ListItem target = mContainer.Children[(mFirstBindedIndex+4)%5] as ListItem;

                int targetIndex = mFirstBindedIndex-1;
                mAdaptor.BindDataWithItem(target,targetIndex);
                target.Position = new Position(0,(int)((targetIndex)*mListItemSize.Height));
            }

            mFirstBindedIndex = candidateStart;
        }

        public void SetAdaptor(Adaptor adaptor){
            mAdaptor = adaptor;
        }
        public Adaptor GetAdaptor(){
            return mAdaptor;
        }
    }
}