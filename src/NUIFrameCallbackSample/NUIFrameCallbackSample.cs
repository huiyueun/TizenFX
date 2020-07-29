using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIFrameCallbackSample
{
    class Program : NUIApplication
    {
        private TextLabel text;
        public class FrameCallback : FrameCallbackInterface
        {
            public uint text_id;
            private int pos_y = 0;
            public FrameCallback()
            {

            }


            public override void OnUpdate(float elapsedSeconds)
            {
                Tizen.Log.Error("MYLOG", "OnUpdate : " + elapsedSeconds);
                Vector3 vector = new Vector3();
                GetPosition(text_id, vector);

                Tizen.Log.Error("MYLOG", "text id : " + text_id);
                Tizen.Log.Error("MYLOG", "vector position : " + vector.X + "," + vector.Y);


                SetPosition(text_id, new Vector3(0.0f, pos_y++, 0.0f));

                if(pos_y>1920)
                {
                    pos_y = 0;
                }

            }
        }
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {
            Window.Instance.KeyEvent += OnKeyEvent;

            text = new TextLabel("Hello Tizen NUI World");
            text.Position = new Position(40, 40);
            text.HorizontalAlignment = HorizontalAlignment.Center;
            text.VerticalAlignment = VerticalAlignment.Center;
            text.TextColor = Color.Blue;
            text.PointSize = 12.0f;
            text.HeightResizePolicy = ResizePolicyType.FillToParent;
            text.WidthResizePolicy = ResizePolicyType.FillToParent;
            Window.Instance.GetDefaultLayer().Add(text);

            Animation animation = new Animation(2000);
            animation.AnimateTo(text, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.X), 0, 500);
            animation.AnimateTo(text, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.X), 500, 1000);
            animation.Looping = true;
            animation.Play();


            FrameCallback cb = new FrameCallback();
            cb.text_id = text.ID;
            Window.Instance.AddFrameCallback(cb);
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
