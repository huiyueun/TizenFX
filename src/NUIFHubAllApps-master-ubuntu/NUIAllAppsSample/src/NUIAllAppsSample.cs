using Tizen.NUI;
using Tizen.NUI.Components;

namespace NUIAllAppsSample
{
    class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            //Load Font Style
            //FontClient.Instance.AddCustomFontDirectory( "./res/font/");

            //Set Broker Application
            //ApplicationTransitionManager.Instance.ApplicationFrameType = FrameType.FrameBroker;

            InitWindow();

            GetDefaultWindow().GetDefaultNavigator().Push(new MainPage());
        }

        private void InitWindow()
        {
            GetDefaultWindow().AddAvailableOrientation(Window.WindowOrientation.Portrait);
            GetDefaultWindow().SetPreferredOrientation(Window.WindowOrientation.Portrait);

            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.Title = "Home";
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
            new Program().Run(args);
        }
    }
}
