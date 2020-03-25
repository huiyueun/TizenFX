
using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;

namespace Tizen.NUI
{
    public class RotaryEditMode : IRotaryTouchController
    {
        internal RotarySelectorItem SelectedItem { get; set;}
        internal RotarySelectorItem CollisionItem { get; set;}

        public delegate void FinishEditingIconHandler(RotarySelectorItem item);
        public event FinishEditingIconHandler FinishEditing;

        private bool isProcessing = false;
        private int cancelIdx = 0;

        public void ProcessTouchEvent(RotarySelectorItem item)
        {
            if(SelectedItem == null)
            {
                Tizen.Log.Error("MYLOG", "start edit mode\n");
                SelectedItem = item;
                SelectedItem.BackgroundColor = Color.Blue;
                Window.Instance.TouchEvent += Instance_TouchEvent;
            }

        }
        public void ProcessMotionEvent(int currentPage, List<RotaryItemWrapper> wrapperList, RotarySelectorItem item)
        {
            if(item == SelectedItem)
            {
                Tizen.Log.Error("MYLOG", "Same object\n");
                return;
            }

            if(!isProcessing && SelectedItem != null)
            {
                Tizen.Log.Error("MYLOG", "Collision\n");
                isProcessing = true;
                RotarySelectorItem collisionItem = item;
                
                collisionItem.BackgroundColor = Color.Red;
                //DisconnectItemTouchEvent(item);

                int page = (currentPage % 2) * 11;
                int selIdx = (int)SelectedItem?.CurrentIndex;
                int colIdx = (int)collisionItem?.CurrentIndex;
                item.BackgroundColor = Color.Red;
                if(selIdx < colIdx)
                {
                    for(int i = selIdx; i < colIdx; i++)
                    {
                        int idx = page + i;
                        wrapperList[idx].SetCurrentItem(wrapperList[idx + 1].GetCurrentItem());
                        wrapperList[idx].GetCurrentItem().BackgroundColor = Color.Red;
                        wrapperList[idx].PlayRotaryPathAnimation(200);
                    } 
                }
                else
                {
                    for(int i = selIdx; i > colIdx; i--)
                    {
                        int idx = page + i;
                        wrapperList[idx].SetCurrentItem(wrapperList[idx - 1].GetCurrentItem());
                        wrapperList[idx].GetCurrentItem().BackgroundColor = Color.Red;
                        wrapperList[idx].PlayRotaryPathAnimation(200, false);
                    } 
                }
                wrapperList[page + colIdx].SetCurrentItem(SelectedItem);
                Timer animationProcessTimer = new Timer(200);
                animationProcessTimer.Tick += Timer_Tick;
                animationProcessTimer.Start();
                return;
            }
            return;
        }

        private bool Timer_Tick(object source, Timer.TickEventArgs e)
        {
            Tizen.Log.Error("MYLOG","timer tick \n");
            isProcessing = false;
            return false;
        }

        private void Instance_TouchEvent(object sender, Window.TouchEventArgs e)
        {
            if(SelectedItem != null)
            {
                if (e.Touch.GetState(0) == PointStateType.Down)
                {
                }
                else if ((e.Touch.GetState(0) == PointStateType.Up))
                {
                    Tizen.Log.Error("MYLOG", "Finish edit mode\n");
                    SelectedItem.RaiseToTop();
                    Window.Instance.TouchEvent -= Instance_TouchEvent;
                    if(FinishEditing != null)
                    {
                        FinishEditing(SelectedItem);
                    }
                    SelectedItem = null;
                }
                else if ((e.Touch.GetState(0) == PointStateType.Motion))
                {
                    SelectedItem.Position = new Position(e.Touch.GetScreenPosition(0).X, e.Touch.GetScreenPosition(0).Y);
                }

            }

        }
    }
}