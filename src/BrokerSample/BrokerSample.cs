using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace BrokerSample
{
    class Program : NUIApplication
    {
        private Window window;
        private View button;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            window = GetDefaultWindow();
            window.KeyEvent += OnKeyEvent;

            CreateUI();
            SetApplicationTransition();
        }

        private void CreateUI()
        {
            var layoutView = new View()
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundColor = Color.Blue,
                Layout = new AbsoluteLayout(),
            };

            button = new View()
            {
                BackgroundColor = Color.Cyan,
                Size = new Size(100, 100),
                PivotPoint = PivotPoint.Center,
                ParentOrigin = ParentOrigin.Center,
                PositionUsesPivotPoint = true,
                CornerRadius = new Vector4(0.5f, 0.5f, 0.5f, 0.5f),
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
            };
            layoutView.Add(button);
            button.TouchEvent += targetView_TouchEvent;
            Window.Instance.Add(layoutView);
        }

        private void SetApplicationTransition()
        {
            var appTransition = ApplicationTransitionManager.Instance;
            appTransition.ApplicationFrameType = FrameType.FrameBroker;
            appTransition.SourceView = button;

            //You can set TransitionBase animation such as SlideTransition, ScaleTransition ..
            /*
            appTransition.AppearingTransition = new SlideTransition()
            {
                TimePeriod = new TimePeriod(500),
                AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Default),
                Direction = SlideTransitionDirection.Right,
            };
            appTransition.DisappearingTransition = new SlideTransition()
            {
                TimePeriod = new TimePeriod(500),
                AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Default),
                Direction = SlideTransitionDirection.Left,
            };*/
        }

        private bool targetView_TouchEvent(object source, View.TouchEventArgs e)
        {
            //앱 런칭
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                ApplicationTransitionManager.Instance.LaunchRequestWithTransition(new AppControl
                {
                    ApplicationId = "org.tizen.example.ProviderSample",
                });
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
