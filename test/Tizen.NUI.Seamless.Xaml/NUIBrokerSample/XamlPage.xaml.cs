using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
{
    public partial class XamlPage : View
    {
        public bool isStartingProcess = false;
        private Vector2 prePos = new Vector2(0, 0);
        private Vector2 firstPos = new Vector2(0, 0);
        private Animation startAni;
        private NUIApplication application;

        public XamlPage(NUIApplication application)
        {
            this.application = application;
            InitializeComponent();
        }

        private bool OnViewTouchEvent(object source, TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                prePos = e.Touch.GetScreenPosition(0);
                firstPos = prePos;

                startAni = new Animation(150);
                startAni.AnimateTo(MainView, "Scale", new Vector3(0.9f, 0.9f, 1.0f));
                startAni.AnimateTo(AnimationView, "Scale", new Vector3(0.9f, 0.9f, 1.0f));
                startAni.Play();
            }
            else if (e.Touch.GetState(0) == PointStateType.Motion)
            {
                Vector2 curPos = e.Touch.GetScreenPosition(0);
                float moveX = curPos.X - prePos.X;
                float moveY = curPos.Y - prePos.Y;
                Position mPos = MainView.Position;
                MainView.Position = new Position(mPos.X + moveX, mPos.Y + moveY);
                AnimationView.Position = new Position(mPos.X + moveX, mPos.Y + moveY);

                prePos = curPos;
            }
            else if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Vector2 curPos = e.Touch.GetScreenPosition(0);
                float moveX = Math.Abs(curPos.X - firstPos.X);
                float moveY = Math.Abs(curPos.Y - firstPos.Y);

                if (moveX < 5 && moveY < 5 && !isStartingProcess)
                {
                    Tizen.Log.Error("MYLOG", "launch app");
                    LaunchApplication();
                }
                if (startAni != null)
                {
                    startAni.Clear();
                    startAni.Dispose();
                    startAni = null;
                }
                startAni = new Animation(150);
                startAni.AnimateTo(MainView, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                startAni.AnimateTo(AnimationView, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                startAni.Play();
            }
            return true;
        }

        private void LaunchApplication()
        {
            AppControl appControl = new AppControl();
            appControl.ApplicationId = "org.tizen.example.NUIMusicPlayer";
            application.SendLaunchRequest(appControl);

        }
    }
}
