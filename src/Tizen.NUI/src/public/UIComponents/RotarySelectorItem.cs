
using System;
using System.ComponentModel;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    public class RotarySelectorItem : View
    {
        private string mainText;
        private string subText;
        
        private Color MainTextColor;
        private Color subTextColor;

        private bool isDeleteEnabled;
        private bool isAddEnabled;
        
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
        
    }
}