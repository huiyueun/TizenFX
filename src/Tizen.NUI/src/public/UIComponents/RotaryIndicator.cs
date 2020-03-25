
/* Copyright (c) 2020 Samsung Electronics Co., Ltd.
.*
.* Licensed under the Apache License, Version 2.0 (the "License");
.* you may not use this file except in compliance with the License.
.* You may obtain a copy of the License at
.*
.* http://www.apache.org/licenses/LICENSE-2.0
.*
.* Unless required by applicable law or agreed to in writing, software
.* distributed under the License is distributed on an "AS IS" BASIS,
.* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
.* See the License for the specific language governing permissions and
.* limitations under the License.
.*
.*/

using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    public class RotaryIndicator : View
    {
        private int currentIndex = -1;
        private int goToIndex = 0;
        private Animation ani;
        public RotaryIndicator()
        {
            ani = new Animation(250);
            Size = new Size(10, 10);
            BackgroundColor = Color.Red;
            PivotPoint = Tizen.NUI.PivotPoint.Center;
            PositionUsesPivotPoint = true;
        }

        public void SetRotaryPosition(uint i)
        {
            if(currentIndex == i)
            {
                return;
            }
            goToIndex = (int)i;
            Size size = new Size(480, 600);
            float radius = (size.Width < size.Height) ? size.Width/2 : size.Height/2;
            //radius -= 200;
            uint index = i+1;
            float calValue = (float)index / 12;

            float x = (float)(size.Width/2 + 150 * Math.Cos((float)index / 12 * 2 * Math.PI - Math.PI / 2));
            float y = (float)(size.Height/2 + 150 * Math.Sin((float)index  / 12 * 2 * Math.PI - Math.PI / 2));
            this.Position = new Position(x,y);

            PlayRotaryPathAnimation();
            currentIndex = goToIndex;
        }

        internal Position GetRotaryPosition(float i)
        {
            Size size = new Size(480, 600);

            float radius = (size.Width < size.Height) ? size.Width/2 : size.Height/2;

            float x = (float)(size.Width / 2 + 150 * Math.Cos((float)i / 12 * 2 * Math.PI - Math.PI / 2));
            float y = (float)(size.Height / 2 + 150 * Math.Sin((float)i  / 12 * 2 * Math.PI - Math.PI / 2));
            return new Position(x, y);
        }

        internal Path GetRotaryPositionPathIndex(bool isReverse = true)
        {
            int sidx = currentIndex + 1;
            int eidx = goToIndex + 1;

            Path path = new Path();
            //move index 2 to 1 , R: 0 to 1
            
            if(sidx < eidx)
            {
                for (int j = sidx; j <= eidx; j++)
                {
                    path.AddPoint(GetRotaryPosition(j));
                }
            }
            else
            {
                for (int j = sidx; j >= eidx; j--)
                {
                    path.AddPoint(GetRotaryPosition(j));
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }

        internal void PlayRotaryPathAnimation(bool isReverse = true)
        {
            ani.Clear();
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            ani.AnimatePath(this, GetRotaryPositionPathIndex(isReverse), Vector3.Zero);

            ani.Play();
            //ani.AnimateTo(this, "Position",new Position(x,y));
        }

    }

}