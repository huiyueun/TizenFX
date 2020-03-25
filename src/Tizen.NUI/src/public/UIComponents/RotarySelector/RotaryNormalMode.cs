using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;
using System.Collections.Generic;

namespace Tizen.NUI
{
    internal class RotaryNormalMode : IRotaryTouchController
    {
        
        internal RotarySelectorItem SelectedItem { get; set;}
        public void ProcessTouchEvent(RotarySelectorItem item)
        {
            if(SelectedItem == null)
            {
                SelectedItem = item;
                SelectedItem.Selected();
                item.BackgroundColor = Color.Cyan;
                Animation ani = new Animation(150);
                ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
                ani.AnimateTo(item, "Scale", new Vector3(1.2f, 1.2f, 1.2f));
                ani.Play();

                ani.Finished += ScaleFinishAnimation;

            }
        }
        public void ScaleFinishAnimation(object sender, EventArgs e)
        {
            SelectedItem.BackgroundColor = Color.White;
            Animation ani = new Animation(100);
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            ani.AnimateTo(SelectedItem, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
            ani.Play();
            SelectedItem = null;
        }



        public void ProcessMotionEvent(int currentPage, List<RotaryItemWrapper> wrapperList, RotarySelectorItem item)
        {
            //Nothing
        }
    }
}