using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIMusicPlayer
{
    public partial class XamlPage : View
    {
        public XamlPage()
        {
            InitializeComponent();
        }

        private bool OnBackTouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                //HideObjects();
                //Window.Instance.SetIconified(true);
                //Exit();
            }
            return false;
        }
    }
}
