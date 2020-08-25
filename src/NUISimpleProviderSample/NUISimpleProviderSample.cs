using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUISimpleProviderSample
{
    class Program : NUIApplication
    {
        private TextLabel text;
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.TouchEvent += Instance_TouchEvent;
            Window.Instance.BackgroundColor = Color.Red;

            View v = new View();
            v.Size = new Size(100, 100);
            v.BackgroundColor = Color.White;
            Window.Instance.Add(v);

            text = new TextLabel("Hello Provider Simple");
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextColor = Color.White;
            text.PointSize = 10.0f;
            text.HeightResizePolicy = ResizePolicyType.FillToParent;
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(text);

            SetFrameProvider();
            frameProvider.Shown += FrameProvider_Shown;
            frameProvider.Hidden += FrameProvider_Hidden;
        }

        private void Instance_TouchEvent(object sender, Window.TouchEventArgs e)
        {
            //Window.Instance.Hide();
            Window.Instance.SetIconified(true);
        }

        private void FrameProvider_Hidden(object sender, EventArgs e)
        {
            text.Hide();
            Bundle bundle = new Bundle();
            frameProvider.NotifyHideStatus(bundle);
        }

        private void FrameProvider_Shown(object sender, EventArgs e)
        {
            text.Show();
            Bundle bundle = new Bundle();
            frameProvider.NotifyShowStatus(bundle);
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
