using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIMusicPlayer
{
    public class Program : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Window window = Window.Instance;
            window.KeyEvent += OnKeyEvent;

            XamlPage page = new XamlPage();
            page.PositionUsesPivotPoint = true;
            page.ParentOrigin = ParentOrigin.TopLeft;
            page.PivotPoint = PivotPoint.TopLeft;
            page.BackgroundColor = Color.Black;
            page.Size = new Size(window.WindowSize.Width, window.WindowSize.Height, 0);
            window.Add(page);

            TransitionOptions = new TransitionOptions(window);
            TransitionOptions.EnableTransition = true;

        }

        protected override void OnAppControlReceived(AppControlReceivedEventArgs e)
        {
            base.OnAppControlReceived(e);

            Window.Instance.Show();
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Window.Instance.Hide();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
