
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
        private int lastPage = 0;
        private uint currentWrapIdx = 0;
        private Size rotarySize;

        private int itemTotalCount = 0;
        

        internal RotarySelectorManager2(Size rotarySize)
        {
            this.rotarySize = rotarySize;

            wrapperList = new List<RotaryItemWrapper>();

            // First Page
            for(uint i = 0; i < MAX_ITEM_COUNT; i++)
            {
                RotaryItemWrapper rotaryItemWrapper = new RotaryItemWrapper();
                rotaryItemWrapper.CurrentIndex = i;
                rotaryItemWrapper.isHidden = false;
                wrapperList.Add(rotaryItemWrapper);
            }

            // Second Page -> hide
            for(uint i = 0; i < MAX_ITEM_COUNT; i++)
            {
                RotaryItemWrapper rotaryItemWrapper = new RotaryItemWrapper();
                rotaryItemWrapper.CurrentIndex = i;
                rotaryItemWrapper.isHidden = true;
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
            else
            {
                item.Hide();
            }
            itemTotalCount++;

            int remainder = 0;
            lastPage = Math.DivRem(itemTotalCount, 11, out remainder);
            lastPage += (remainder > 0) ? 1 : 0;
        }

        internal void NextPage(List<RotarySelectorItem> itemList)
        {
            if(currentPage + 1 < lastPage)
            {
                int sIdx = ((int)currentPage % 2) * 11;
                int eIdx = ((int)(currentPage + 1) % 2) * 11;
                int setIdx = (int)(currentPage + 1) * 11;

                for(int i = sIdx, j = eIdx; i < sIdx + 11; i++, j++)
                {
                    wrapperList[i].PlayRotaryPageHideAnimation(400, false);
                    wrapperList[i].isHidden = true;

                    if(setIdx < itemTotalCount)
                    {
                        wrapperList[j].SetCurrentItem(itemList[setIdx++]);
                        wrapperList[j].ShowItem();
                        wrapperList[j].PlayRotaryPageAnimation(400, false);
                        wrapperList[j].isHidden = false;
                    }
                }

                currentPage++;
            }
        }

        internal void PrevPage(List<RotarySelectorItem> itemList)
        {
            if(currentPage > 0)
            {
                int sIdx = ((int)currentPage % 2) * 11;
                int eIdx = ((int)(currentPage - 1) % 2) * 11;
                int setIdx = (int)(currentPage - 1) * 11;

                for(int i = sIdx, j = eIdx; i < sIdx + 11; i++, j++)
                {
                    wrapperList[i].PlayRotaryPageHideAnimation(800);
                    wrapperList[i].isHidden = true;

                    wrapperList[j].SetCurrentItem(itemList[setIdx++]);
                    wrapperList[j].ShowItem();
                    wrapperList[j].PlayRotaryPageAnimation(800);
                    wrapperList[j].isHidden = false;
                }
                currentPage--;
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
        
        internal bool Item_TouchEvent(object source, View.TouchEventArgs e)
        {
            RotarySelectorItem item = source as RotarySelectorItem;
            if ((e.Touch.GetState(0) == PointStateType.Down))
            {
                rotaryTouchController.ProcessTouchEvent(item);
            }
            if ((e.Touch.GetState(0) == PointStateType.Motion))
            {
                rotaryTouchController.ProcessMotionEvent((int)currentPage, wrapperList, item);
            }
            return false;
        }

        private void EiditingFinish(RotarySelectorItem item)
        {
            int wrapperIdx = (int)(currentPage % 2) * 11;
            wrapperList[wrapperIdx + (int)item.CurrentIndex].SetCurrentItem(item, true);
            //wrapperList[(int)item.CurrentIndex].GetCurrentItem().BackgroundColor = Color.White;
            foreach(RotaryItemWrapper wrapper in wrapperList)
            {
                if(wrapper.GetCurrentItem() != null)
                    wrapper.GetCurrentItem().BackgroundColor = Color.White;
            }
        }

    }
}