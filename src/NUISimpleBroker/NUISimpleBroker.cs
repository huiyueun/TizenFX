using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUISimpleBroker
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;

            TextLabel text = new TextLabel("Hello Tizen NUI World");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextColor = Color.White;
            text.PointSize = 10.0f;
            text.HeightResizePolicy = ResizePolicyType.FillToParent;
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(text);

            text.TouchEvent += Text_TouchEvent;
            SetFramerBroker(new AnimationBroker(Window.Instance));
        }

        private bool Text_TouchEvent(object source, View.TouchEventArgs e)
        {
            if(e.Touch.GetState(0) == PointStateType.Up)
            {
                Tizen.Log.Error("MYLOG", "Touch Up");
                AppControl appControl = new AppControl();
                appControl.ApplicationId = "org.tizen.example.NUISimpleProviderSample";
                SendLaunchRequest(appControl, true);
            }
            return false;
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
