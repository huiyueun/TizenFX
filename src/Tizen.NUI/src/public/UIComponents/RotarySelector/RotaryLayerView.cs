using System;
using System.ComponentModel;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;


namespace Tizen.NUI
{
    internal class RotaryLayerView : View
    {
        private List<RotarySelectorItem> itemList;

        private TextLabel mainText;
        private TextLabel subText;
        private RotaryIndicator rotaryIndicator;

        internal RotaryLayerView()
        {
            itemList = new List<RotarySelectorItem>();
            mainText = new TextLabel()
            {
                Text = "MainText",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                TextColor = Color.White,
                PointSize = 30.0f,
            };
            this.Add(mainText);
            subText = new TextLabel()
            {
                Text = "Sub",

                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                TextColor = Color.White,
                PointSize = 18.0f,
                Position = new Position(0, 50),
            };
            this.Add(subText);

            rotaryIndicator = new RotaryIndicator();
            rotaryIndicator.SetRotaryPosition(0);
            this.Add(rotaryIndicator);

        }
        
        internal List<RotarySelectorItem> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
            }
        }

        internal void ChangeItemCallback(RotarySelectorItem item)
        {
            this.mainText.Text = item.MainText;
            this.subText.Text = item.SubText;

            rotaryIndicator.SetRotaryPosition(item.CurrentIndex);
        }

        internal void ChangeMode(bool isEditMode)
        {
            if(isEditMode)
            {
                this.mainText.Text = "Edit Mode";
                this.subText.Text = "";
                rotaryIndicator.Hide();
            }
            else
            {
                this.mainText.Text = itemList[0].MainText;
                this.subText.Text = itemList[0].SubText;
                rotaryIndicator.Show();
                
            }
        }

        internal void PlayIndicatorAnimation()
        {
            Animation ani = new Animation(150);
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            ani.AnimateTo(this.rotaryIndicator, "Opacity", 0.0f);
            ani.Play();

            ani.Finished += Ani_Finished;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            Animation ani = new Animation(100);
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            ani.AnimateTo(this.rotaryIndicator, "Opacity", 1.0f);
            ani.Play();
        }

        internal void AppendItem(RotarySelectorItem item)
        {
            item.ItemSelected += ChangeItemCallback;
            itemList.Add(item);
            item.MyParent = this;
            //item.TouchEvent += Item_TouchEvent;    

            item.PivotPoint = Tizen.NUI.PivotPoint.Center;
            item.PositionUsesPivotPoint = true;
            //this.Add(item);

        }
        internal void PrependItem(RotarySelectorItem item)
        {
            itemList.Insert(0, item);
        }

        internal void InsertItem(int index, RotarySelectorItem item)
        {
            itemList.Insert(index, item);
        }

        internal void DeleteItem(RotarySelectorItem item)
        {
            itemList.Remove(item);
        }

        internal void DeleteItemIndex(int index)
        {
            itemList.RemoveAt(index);
        }

        internal void ClearItem()
        {
            itemList.Clear();
        }


        
    }
}