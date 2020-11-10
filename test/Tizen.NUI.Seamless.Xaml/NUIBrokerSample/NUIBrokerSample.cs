using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
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
