
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;


namespace Tizen.NUI
{
    public class RotarySelector2 : View
    {
        private RotarySelectorManager2 rotarySelectorManager;

        //View
        private RotaryLayerView rotaryLayerView;

        private bool isEditMode = false;

        public RotarySelector2()
        {
            rotarySelectorManager = new RotarySelectorManager2(new Size(480, 600));

            rotaryLayerView = new RotaryLayerView()
            {
                WidthResizePolicy = ResizePolicyType.FillToParent,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };
            this.Add(rotaryLayerView);
        }

        public List<RotarySelectorItem> GetRotarySelectorItems()
        {
            return rotaryLayerView.ItemList;
        }

        public void AppendItem(RotarySelectorItem item)
        {
            rotaryLayerView.AppendItem(item);
            rotarySelectorManager.WrappingItem(item);
            rotarySelectorManager.ConnectItemTouchEvent(item);
        }

        public void PrependItem(RotarySelectorItem item)
        {
            rotaryLayerView.PrependItem(item);
        }

        public void InsertItem(int index, RotarySelectorItem item)
        {
            rotaryLayerView.InsertItem(index, item);
        }

        public void DeleteItem(RotarySelectorItem item)
        {
            rotaryLayerView.DeleteItem(item);
        }

        public void DeleteItemIndex(int index)
        {
            rotaryLayerView.DeleteItemIndex(index);
        }

        public void ClearItem()
        {
            rotaryLayerView.ClearItem();
        }

        public void NextPage()
        {
            rotaryLayerView.PlayIndicatorAnimation();
            rotarySelectorManager.NextPage(rotaryLayerView.ItemList);
        }

        public void PrevPage()
        {
            rotaryLayerView.PlayIndicatorAnimation();
            rotarySelectorManager.PrevPage(rotaryLayerView.ItemList);
        }

        public bool IsEditMode
        {
            get
            {
                return isEditMode;
            }
            set
            {
                isEditMode = value;
                rotarySelectorManager.SetRotarySelectorMode(isEditMode);
                rotaryLayerView.ChangeMode(isEditMode);
            }
        }



    }
}