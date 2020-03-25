
using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    public class RotarySelectorItem : View
    {
        internal View MyParent {get; set;}
        private string mainText;
        private string subText;
        
        private Color MainTextColor;
        private Color subTextColor;

        private bool isDeleteEnabled;
        private bool isAddEnabled;

            
        public delegate void ItemSelectedHandler(RotarySelectorItem item);
        public event ItemSelectedHandler ItemSelected;

        public string MainText
        {
            get
            {
                return mainText;
            }
            set
            {
                mainText = value;
            }
        }


        public string SubText
        {
            get
            {
                return subText;
            }
            set
            {
                subText = value;
            }
        }
        
        internal uint CurrentIndex { get; set; }

        public RotarySelectorItem()
        {
        }

        internal void Selected()
        {
            ItemSelected(this);
        }
    }
}