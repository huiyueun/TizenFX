using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace ProviderSample
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

            CreateUI();

            ApplicationTransitionManager.Instance.ApplicationFrameType = FrameType.FrameProvider;
        }

        private void CreateUI()
        {
            var layoutView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Red,
                Layout = new AbsoluteLayout(),
            };
            layoutView.TouchEvent += ContentPage_TouchEvent;
            Window.Instance.Add(layoutView);
        }

        private bool ContentPage_TouchEvent(object source, View.TouchEventArgs e)
        {
            //앱 런칭
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Window.Instance.Lower();
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
