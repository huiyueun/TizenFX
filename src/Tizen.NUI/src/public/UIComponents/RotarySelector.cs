
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
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    public class RotarySelector : View
    {
        private ImageView contentView;
        private View iconLayerView;
        private TextLabel mainText;
        private TextLabel subText;

        private List<RotarySelectorItem> itemList;

        private ContentType contentType;

        private bool isEditMode = true;

        private Color backgroundColor;

        private bool isEffect;

        private int currentPage = 1;
        private int numberOfDrawObjects = 11;
        private int maxPage = 1;

        //private RotaryIndicator rotaryIndicator;
        public RotarySelector(ContentType contentType = ContentType.MainTextOnly)
        {
            Initialize();

            //rotaryIndicator = new RotaryIndicator();
            //rotaryIndicator.CreateIndicator(this);
        }

        public List<RotarySelectorItem> GetRotarySelectorItems()
        {
            return itemList;
        }

        public void AppendItem(RotarySelectorItem item)
        {
            itemList.Add(item);
            item.TouchEvent += Item_TouchEvent;    
            iconLayerView.Add(item);
            DrawCircurlarIcon();
        }

        public void PrependItem(RotarySelectorItem item)
        {
            itemList.Insert(0, item);
        }

        public void InsertItem(int index, RotarySelectorItem item)
        {
            itemList.Insert(index, item);
        }

        public void DeleteItem(RotarySelectorItem item)
        {
            itemList.Remove(item);
        }

        public void DeleteItemIndex(int index)
        {
            itemList.RemoveAt(index);
        }

        public void ClearItem()
        {
            itemList.Clear();
        }

        public ContentType ContnetType
        {
            get
            {
                return contentType;
            }
            set
            {
                contentType = value;
            }
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
            }
        }
        public bool IsEffect
        {
            get
            {
                return isEffect;
            }
            set
            {
                isEffect = value;
            }
        }

        public enum ContentType
        {
            MainTextOnly,
            MainTextSubText,
            IconOnly,
            IconMainText,
        }

        private void Initialize()
        {
            itemList = new List<RotarySelectorItem>();
            iconLayerView = new View()
            {
                WidthResizePolicy = ResizePolicyType.FillToParent,
                HeightResizePolicy = ResizePolicyType.FillToParent,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };

            this.Add(iconLayerView);
            CreateContent();

            DrawCircurlarIcon();
        }

        private void CreateContent()
        {
            switch (contentType)
            {
                case ContentType.MainTextOnly:
                    mainText = new TextLabel()
                    {
                        Text = "MainText",
                        WidthResizePolicy = ResizePolicyType.FillToParent,
                        HeightResizePolicy = ResizePolicyType.FillToParent,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        TextColor = Color.White,
                        PointSize = 20.0f,
                    };
                    this.Add(mainText);
                break;
            }
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0f;
        }

        public bool NextPage()
        {   
            if(currentPage + 1 <= maxPage)
            {
                currentPage++;
                return DrawCircurlarIcon(true);
            }

            return false;
        }

        public bool PrevPage()
        {   
            if(currentPage - 1 >= 0)
            {
                currentPage--;
                return DrawCircurlarIcon(false);
            }
            return false;
        }

        private void PlayRotaryAnimation(RotarySelectorItem item, bool isNextPage, uint page, uint itemIdx)
        {
            uint idx = itemIdx;//(itemIdx == 0) ? 0u : 1u;
            int cnt = 12;
            int startDegree = 60;
            bool isReverse = false;
            if ( page == currentPage )
            {
                //item.RaiseToTop();
            }
            if ( page < currentPage )
            {
                //cnt = 120;
                //startDegree = 70;
                //item.RaiseToTop();
                isReverse = true;
            }
            else if ( page > currentPage )
            {
                //cnt = 120;
                //startDegree = 113;
                //item.LowerToBottom();
                isReverse = true;
            }


            Path path = GenerateRotatePath(isReverse, isNextPage, idx, cnt, startDegree);
            Animation ani = new Animation(450);
            ani.SetDefaultAlphaFunction(new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare));
            if(isReverse)
            {
                ani.AnimatePath(item, path, Vector3.Zero, 0, 250);
                ani.AnimateTo(item,"Opacity", 0.0f, 0, 100);
            }
            else
            {
                ani.AnimatePath(item, path, Vector3.Zero, 200, 450);
                ani.AnimateTo(item,"Opacity", 1.0f, 200, 300);
            }
            ani.Play();

        }

        private Path GenerateRotatePath(bool isReverse, bool isNextPage, uint idx, int maxCnt, int startDegree)
        {
            Path path = new Path();
            if(isReverse)
            {
                for (int rotary_path = 20; rotary_path >= 0; rotary_path--)
                {
                    float calValue = ((float)(idx)/20 * rotary_path ) / (maxCnt);
                    path.AddPoint(GetCurrentRotaryPosition(!isNextPage, calValue, startDegree));
                }
            }
            else
            {
                //for (int rotary_path = 20; rotary_path >= 0; rotary_path--)
                for (int rotary_path = 0; rotary_path <= 20; rotary_path++)
                {
                    float calValue = ((float)(idx)/20 * rotary_path ) / (maxCnt);
                    path.AddPoint(GetCurrentRotaryPosition(isNextPage, calValue, startDegree));
                }
            }
            path.GenerateControlPoints(0);
            return path;
        }

        private Position GetCurrentRotaryPosition(bool isNextPage, float calValue, int startDegree)
        {
            float radius = (this.SizeWidth < this.SizeHeight) ? this.SizeWidth/2 : this.SizeHeight/2;
            radius -= itemList[0].SizeWidth;
            
            Position position = new Position();
            if(isNextPage)
            {
                position.X = (float)(this.SizeWidth/2 + radius * -Math.Cos((calValue * DegreeToRadian(360) - DegreeToRadian(startDegree) )));
                position.Y = (float)(this.SizeHeight/2 + radius * Math.Sin((calValue * DegreeToRadian(360) - DegreeToRadian(startDegree) )));
            }
            else
            {
                position.X = (float)(this.SizeWidth/2 + radius * Math.Cos((calValue * DegreeToRadian(360) - DegreeToRadian(startDegree) )));
                position.Y = (float)(this.SizeHeight/2 + radius * Math.Sin((calValue * DegreeToRadian(360) - DegreeToRadian(startDegree) )));
            }
            return position;
        }

        public bool DrawCircurlarIcon(bool isNextPage = true)
        {
            int numberOfitems = itemList.Count;
            if(numberOfitems == 0)
            {
                Tizen.Log.Error("NUI", "item list is zero");
                return false;
            }

            int remainder = 0;
            maxPage = Math.DivRem(numberOfitems, numberOfDrawObjects, out remainder);

            maxPage += (remainder > 0) ? 1:0;

            if( currentPage > maxPage || currentPage <= 0)
            {
                Tizen.Log.Error("NUI", "page error");
                return false;
            }

            int count = 0;
            for( var i = 1u; i <= maxPage; i++ )
            {
                // Iterate columns
                for( var j = 0u; j < numberOfDrawObjects; j++ )
                {

                    int itemIdx = (int)(isNextPage ? count : count);
                    RotarySelectorItem item = itemList[itemIdx];
                    item.PivotPoint = Tizen.NUI.PivotPoint.Center;
                    item.PositionUsesPivotPoint = true;
                    
                    if(isNextPage)
                        item.CurrentIndex = (uint)numberOfDrawObjects-1-j;
                    else
                        item.CurrentIndex = j;
                    

                    PlayRotaryAnimation(item, isNextPage, i, j);

                    count++;
                    if(count >= numberOfitems)
                    {
                        break;
                    }
                }
            }
            return true;
        }


        View tempIconView;
        RotarySelectorItem selectedItem;
        uint tempSelectIdx;
        bool isProcessed = false;
        private bool Item_TouchEvent(object source, View.TouchEventArgs e)
        {
            if(!isEditMode)
            {
                RotarySelectorItem item = source as RotarySelectorItem;
                //rotaryIndicator.SetRotaryPosition(item.CurrentIndex);
                //item.AnimateSelectAnimation();
            }
            else
            {
                if(selectedItem == null)
                {
                    selectedItem = source as RotarySelectorItem;
                    tempSelectIdx = selectedItem.CurrentIndex;
                    if(tempIconView==null)
                    {
                        tempIconView = new View()
                        {
                            Size = selectedItem.Size,
                            PositionX = selectedItem.ScreenPosition.X,
                            PositionY = selectedItem.ScreenPosition.Y-25,
                            PivotPoint = Tizen.NUI.PivotPoint.Center,
                            PositionUsesPivotPoint = true,
                            BackgroundColor = Color.Cyan,
                        };
                        iconLayerView.Add(tempIconView);
                        tempIconView.LowerToBottom();

                    }
                    Window.Instance.TouchEvent += Instance_TouchEvent;
                    selectedItem.TouchEvent -= Item_TouchEvent;

                }
                else
                {
                    if(!isProcessed)
                    {
                        isProcessed = true;
                        RotarySelectorItem secondItem = source as RotarySelectorItem;
                        
                        bool isReverse = false;
                        if(selectedItem.CurrentIndex < secondItem.CurrentIndex)
                        {
                            isReverse = true;
                        }
                        else
                        {
                            isReverse = false;
                        }
                        //tempIconView.PositionX = secondItem.ScreenPosition.X;
                        //tempIconView.PositionY = secondItem.ScreenPosition.Y - 30;
                        secondItem.BackgroundColor = Color.Red;
                        //secondItem.SetRotaryPosition(secondItem.CurrentIndex - 1);

                        tempSelectIdx  = secondItem.CurrentIndex;
                        //secondItem.SetRotaryPosition(tempSelectIdx - 1, isReverse);
                        secondItem.CurrentIndex = tempSelectIdx;
                        foreach(RotarySelectorItem item in itemList)
                        {
                            if(item == selectedItem) continue;
                            if(item.Opacity == 1.0f)
                            {
                                if(isReverse)
                                {
                                    if(item.CurrentIndex < secondItem.CurrentIndex && item.CurrentIndex>selectedItem.CurrentIndex)
                                    {
                                        item.BackgroundColor = Color.Red;
                                    //    item.SetRotaryPosition(item.CurrentIndex - 1, isReverse);
                                        item.CurrentIndex = item.CurrentIndex - 1;
                                    }
                                }
                                else
                                {
                                    if(item.CurrentIndex > secondItem.CurrentIndex&& item.CurrentIndex<selectedItem.CurrentIndex)
                                    {
                                        item.BackgroundColor = Color.Red;
                                        //item.SetRotaryPosition(item.CurrentIndex - 1, isReverse);
                                        item.CurrentIndex = item.CurrentIndex - 1;
                                    }

                                }
                            }
                        }

                        
                        Tizen.Log.Error("NUI", "idx : " + secondItem.CurrentIndex + '\n');

                        isProcessed = false;
                    }

                }

            }
            return true;
        }
        private void Instance_TouchEvent(object sender, Window.TouchEventArgs e)
        {
            if(isEditMode && selectedItem != null)
            {
                if (e.Touch.GetState(0) == PointStateType.Down)
                {
                }
                else if ((e.Touch.GetState(0) == PointStateType.Up))
                {
                    //selectedItem.SetRotaryPosition(tempSelectIdx, false, false);
                    //selectedItem.CurrentIndex = tempSelectIdx;
                    selectedItem.RaiseToTop();


                    Window.Instance.TouchEvent -= Instance_TouchEvent;
                    selectedItem.TouchEvent += Item_TouchEvent;
                    selectedItem = null;
                }
                else if ((e.Touch.GetState(0) == PointStateType.Motion))
                {
                    selectedItem.Position = new Position(e.Touch.GetScreenPosition(0).X, e.Touch.GetScreenPosition(0).Y);
                }

            }

        }

    }
}