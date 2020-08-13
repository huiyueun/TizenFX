using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIMusicPlayer
{
    class Program : NUIApplication
    {
        private UICreator uiCreator;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            uiCreator = new UICreator();
            uiCreator.CreateUI();

            SetFrameProvider();
            frameProvider.Shown += FrameProvider_Shown;
            frameProvider.Hidden += FrameProvider_Hidden;
        }

        protected void OnResumed()
        {
            base.OnResume();
            uiCreator.ShowObjects();
        }

        private void FrameProvider_Hidden(object sender, EventArgs e)
        {
            Tizen.Log.Error("MYLOG", "provider - hide");
            uiCreator.HideObjects();

            Bundle bundle = new Bundle();
            frameProvider.NotifyHideStatus(bundle);
        }

        private void FrameProvider_Shown(object sender, EventArgs e)
        {
            Tizen.Log.Error("MYLOG", "provider - show");
            uiCreator.ShowObjects();

            Bundle bundle = new Bundle();
            frameProvider.NotifyShowStatus(bundle);
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                uiCreator.HideObjects();
                Window.Instance.SetIconified(true);
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
