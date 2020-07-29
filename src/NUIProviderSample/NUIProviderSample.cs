using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIProviderSample
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
            Window.Instance.BackgroundColor = new Color(0, 0, 0, 0);
            Window.Instance.SetTransparency(true);
            
            View view = new View()
            {
                Size = new Size(360, 360),
                BackgroundColor = new Color(0.0f, 0.54f, 0.93f, 1.0f),
                CornerRadius = 90.0f,
            };

            view.TouchEvent += View_TouchEvent;
            Window.Instance.GetDefaultLayer().Add(view);

            ImageView testButton1 = new ImageView()
            {
                //Size = new Size(30, 30),
                Position = new Position(0,-100),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
            };
            testButton1.SetImage(GetResourcePath() + "NUIProviderSample.png");
            Window.Instance.GetDefaultLayer().Add(testButton1);

            TextLabel text = new TextLabel("Provider");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextColor = Color.Blue;
            text.PointSize = 12.0f;
            text.HeightResizePolicy = ResizePolicyType.FillToParent;
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(text);

            Animation ani = new Animation(300);
            ani.Looping = true;
            ani.AnimateTo(text, "Position", new Position(3, 4, 0));
            ani.Play();

        }

        private bool View_TouchEvent(object source, View.TouchEventArgs e)
        {
            Window.Instance.Hide();
            //Exit();
            return true;
        }

        public static string GetResourcePath()
        {
            return Tizen.Applications.Application.Current.DirectoryInfo.Resource;
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
