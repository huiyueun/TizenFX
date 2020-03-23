
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
        public void ProcessMotionEvent(List<RotaryItemWrapper> wrapperList, RotarySelectorItem item)
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

                int selIdx = (int)SelectedItem?.CurrentIndex;
                int colIdx = (int)collisionItem?.CurrentIndex;
                item.BackgroundColor = Color.Red;
                if(selIdx < colIdx)
                {
                    for(int i = selIdx; i < colIdx; i++)
                    {
                        wrapperList[i].SetCurrentItem(wrapperList[i+1].GetCurrentItem());
                        wrapperList[i].GetCurrentItem().BackgroundColor = Color.Red;
                        wrapperList[i].PlayRotaryPathAnimation(200);
                    } 
                }
                else
                {
                    for(int i = selIdx; i > colIdx; i--)
                    {
                        wrapperList[i].SetCurrentItem(wrapperList[i-1].GetCurrentItem());
                        wrapperList[i].GetCurrentItem().BackgroundColor = Color.Red;
                        wrapperList[i].PlayRotaryPathAnimation(200, false);
                    } 
                }
                wrapperList[colIdx].SetCurrentItem(SelectedItem);
                

                isProcessing = false;
                return;
            }

            return;
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