
using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    internal class RotarySelectorManager2
    {
        private uint MAX_ITEM_COUNT = 11;
        private IRotaryTouchController rotaryTouchController;

        private List<RotaryItemWrapper> wrapperList;

        private uint currentPage = 0;
        private uint currentWrapIdx = 0;
        private Size rotarySize;

        internal RotarySelectorManager2(Size rotarySize)
        {
            this.rotarySize = rotarySize;

            wrapperList = new List<RotaryItemWrapper>();

            for(uint i = 0; i < MAX_ITEM_COUNT; i++)
            {
                RotaryItemWrapper rotaryItemWrapper = new RotaryItemWrapper();
                rotaryItemWrapper.CurrentIndex = i;
                wrapperList.Add(rotaryItemWrapper);
            }
            rotaryTouchController = new RotaryNormalMode();
        }
        
        internal void WrappingItem(RotarySelectorItem item)
        {
            if(currentWrapIdx < MAX_ITEM_COUNT)
            {
                wrapperList[(int)currentWrapIdx].SetCurrentItem(item, true);
                currentWrapIdx++;
            }
        }
        internal void NextPage()
        {
            foreach(RotaryItemWrapper wrapper in wrapperList)
            {
                wrapper.PlayRotaryPageAnimation(300, false);
            }
        }

        internal void PrevPage()
        {
            foreach(RotaryItemWrapper wrapper in wrapperList)
            {
                wrapper.PlayRotaryPageAnimation(300);
            }
        }

        internal void SetRotarySelectorMode(bool isEditMode)
        {
            if(isEditMode)
            {
                rotaryTouchController = new RotaryEditMode();
                (rotaryTouchController as RotaryEditMode).FinishEditing += EiditingFinish;

            }
            else
            {
                (rotaryTouchController as RotaryEditMode).FinishEditing -= EiditingFinish;
                rotaryTouchController = new RotaryNormalMode();


            }
        }

        internal void ConnectItemTouchEvent(RotarySelectorItem item)
        {
            item.TouchEvent += Item_TouchEvent;
        }
    

        internal void DisconnectItemTouchEvent(RotarySelectorItem item)
        {
            item.TouchEvent -= Item_TouchEvent;
        }
        
        private bool isProcessing = false;

        internal bool Item_TouchEvent(object source, View.TouchEventArgs e)
        {
            RotarySelectorItem item = source as RotarySelectorItem;
            if ((e.Touch.GetState(0) == PointStateType.Down))
            {
                rotaryTouchController.ProcessTouchEvent(item);
            }
            if ((e.Touch.GetState(0) == PointStateType.Motion))
            {
                rotaryTouchController.ProcessMotionEvent(wrapperList, item);
            }
            return false;
        }

        private void EiditingFinish(RotarySelectorItem item)
        {
            wrapperList[(int)item.CurrentIndex].SetCurrentItem(item, true);
            foreach(RotaryItemWrapper wrapper in wrapperList)
            {
                wrapper.GetCurrentItem().BackgroundColor = Color.White;
            }
        }

    }
}