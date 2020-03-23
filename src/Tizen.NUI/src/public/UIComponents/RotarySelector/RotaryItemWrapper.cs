
using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    public class RotaryItemWrapper
    {
        private Size parentSize;
        internal Position Position { get; set; }

        private RotaryItemWrapper prevItem;
        private RotaryItemWrapper nextItem;

        private RotarySelectorItem item;

        internal uint CurrentIndex { get; set; }

        internal RotaryItemWrapper()
        {
            this.parentSize = new Size(480, 600);
        }

        internal RotarySelectorItem GetCurrentItem()
        {
            return item;
        }

        internal void SetCurrentItem(RotarySelectorItem item, bool isSetPosition = false)
        {
            this.item = item;
            this.item.CurrentIndex = CurrentIndex;
            Position = GetRotaryPosition(CurrentIndex + 1);
            if(isSetPosition)
            {
                this.item.Position = Position;
            }
            //PlayRotaryPathAnimation(200);
        }

        internal void PlayRotaryPathAnimation(int time, bool isReverse = true)
        {
            Animation ani = new Animation(time);
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            ani.AnimatePath(this.item, GetRotaryPositionPathIndex(isReverse), Vector3.Zero);
            ani.Play();
            //ani.AnimateTo(this, "Position",new Position(x,y));
        }

        internal void PlayRotaryPageAnimation(int time, bool isReverse = true)
        {
            this.item.Opacity =  0.0f;
            Animation ani = new Animation(time);
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            ani.AnimatePath(this.item, GetRotaryPositionPath(isReverse), Vector3.Zero);
            ani.AnimateTo(this.item, "Opacity", 1.0f, 0, 100);
            ani.Play();
            //ani.AnimateTo(this, "Position",new Position(x,y));
        }

        internal double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0f;
        }

        internal Position GetRotaryPosition(float i)
        {
            Size size = this.parentSize;
            float radius = (size.Width < size.Height) ? size.Width/2 : size.Height/2;

            float x = (float)(size.Width / 2 + 200 * Math.Cos((float)i / 12 * 2 * Math.PI - Math.PI / 2));
         
            float y = (float)(size.Height / 2 + 200 * Math.Sin((float)i  / 12 * 2 * Math.PI - Math.PI / 2));
            return new Position(x, y);
        }

        internal Path GetRotaryPositionPath(bool isReverse = true)
        {
            Path path = new Path();
            float fIndex = CurrentIndex + 1;

            if(isReverse)
            {
                for (int j = 0; j <= fIndex; j++)
                {
                    path.AddPoint(GetRotaryPosition(j));
                    //fIndex += (isReverse ? -0.1f : 0.1f);
                }
            }
            else
            {
                for (int j = 12; j >= fIndex; j--)
                {
                    path.AddPoint(GetRotaryPosition(j));
                    //fIndex += (isReverse ? -0.1f : 0.1f);
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }

        internal Path GetRotaryPositionPathIndex(bool isReverse = true)
        {
            Path path = new Path();
            //move index 2 to 1 , R: 0 to 1
            float fIndex = (float)(isReverse ? CurrentIndex + 2 : CurrentIndex);
            for (int j = 0; j <= 10; j++)
            {
                path.AddPoint(GetRotaryPosition(fIndex));
                fIndex += (isReverse ? -0.1f : 0.1f);
            }
            path.GenerateControlPoints(0);
            return path;
        }

    }
}