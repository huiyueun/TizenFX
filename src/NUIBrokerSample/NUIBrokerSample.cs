using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
{
    class Program : NUIApplication
    {
        private ImageView testButton1;
        private RoundBroker b1;
        private FrameBrokerBase b2;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);
            /*
            TextLabel text = new TextLabel("Broker");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextColor = Color.Black;
            text.PointSize = 8.0f;
            text.HeightResizePolicy = ResizePolicyType.FillToParent;
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            text.Position = new Position(0, -100);
            Window.Instance.GetDefaultLayer().Add(text);
            */
            testButton1 = new ImageView()
            {
                //BackgroundColor = new Color(0.48f, 0.32f, 0.85f, 1.0f),
                Size = new Size(100, 100),
                Position = new Position(0, 100),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                CornerRadius = 50.0f,
        };
            testButton1.SetImage(GetResourcePath() + "NUIBrokerSample.png");
            /*
            View testButton2 = new View()
            {
                BackgroundColor = Color.Blue,
                Size = new Size(100, 50),
                Position = new Position(0, 100),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true
            };
            */
            Window.Instance.Add(testButton1);
            //Window.Instance.Add(testButton2);
            testButton1.TouchEvent += TestButton1_TouchEvent;
            //testButton2.TouchEvent += TestButton2_TouchEvent;

            //b1 = new DefaultAnimationBroker(GetDefaultWindow());
            b1 = new RoundBroker(GetDefaultWindow());
            Window.Instance.SetFramerBroker(b1);
            b1.sharedResource = testButton1;

            //AppControl appControl = new AppControl();
            //appControl.ApplicationId = "org.tizen.example.NUIProviderSample";
            //Window.Instance.SendLaunchRequest(appControl, true);
        }

        public static string GetResourcePath()
        {
            return Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        }
        private bool TestButton1_TouchEvent(object source, View.TouchEventArgs e)
        {
            Animation ani = new Animation(50);
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                ani.AnimateTo(testButton1, "Scale", new Vector3(0.8f, 0.8f, 1.0f));
            }
            else if (e.Touch.GetState(0) == PointStateType.Up)
            {
                ani.AnimateTo(testButton1, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                ani.Finished += Ani_Finished;
            }
            ani.Play();
            return true;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            AppControl appControl = new AppControl();
            appControl.ApplicationId = "org.tizen.example.NUIProviderSample";
            Window.Instance.SendLaunchRequest(appControl, true);
        }


        private bool TestButton2_TouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Tizen.Log.Error("MYLOG", "Btn2 touch");
                if (b1 != null)
                {
                    b1.Dispose();
                    b1 = null;
                }
                if (b2 == null)
                {
                    b2 = new PositionBroker(GetDefaultWindow());
                }
                Window.Instance.SetFramerBroker(b2);

                AppControl appControl = new AppControl();
                appControl.ApplicationId = "org.tizen.example.NUIProviderSample";
                Window.Instance.SendLaunchRequest(appControl, true);
            }
            return true;
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
