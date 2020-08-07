using System;
using System.Collections.Generic;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIFrameComponentBroker
{
    class Program : NUIComponentApplication
    {

        public Program(IDictionary<Type, string> typeInfo) : base(typeInfo)
        {
        }

        class MyFrameComponent : NUIFrameComponent
        {
            private TextLabel text;
            private Animation animation;

            public override bool OnCreate()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent OnCreate");

                Window w = new Window();
                w.WindowSize = new Size(200, 200);
                w.BackgroundColor = Color.Red;
                View v = new View();
                v.Size = new Size(50, 50);
                v.BackgroundColor = Color.Cyan;
                w.Add(v);
                Window.BackgroundColor = Color.White;
                text = new TextLabel("Hello Tizen NUI World");
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.TextColor = Color.Blue;
                text.PointSize = 12.0f;
                text.HeightResizePolicy = ResizePolicyType.FillToParent;
                text.WidthResizePolicy = ResizePolicyType.FillToParent;
                Window.Add(text);

                animation = new Animation(2000);
                animation.AnimateTo(text, "Orientation", new Rotation(new Radian(new Degree(180.0f)), PositionAxis.X), 0, 500);
                animation.AnimateTo(text, "Orientation", new Rotation(new Radian(new Degree(0.0f)), PositionAxis.X), 500, 1000);
                animation.Looping = true;
                animation.Play();
                return true;
            }

            public override void OnDestroy()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent OnDestroy");
                text.Dispose();
                animation.Dispose();
            }

            public override void OnPause()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent OnPause");
            }

            public override void OnResume()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent OnResume");
            }

            public override void OnStart(AppControl appControl, bool restarted)
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent OnStart");
            }

            public override void OnStop()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent OnStop");
            }
        }
        class MyFrameComponent2 : NUIFrameComponent
        {
            private TextLabel text;
            private Animation animation;

            public override bool OnCreate()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent2 OnCreate");
                Window.BackgroundColor = Color.Red;
                Window.WindowSize = new Size(300, 400);
                text = new TextLabel("Hello Tizen NUI World");
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.TextColor = Color.Blue;
                text.PointSize = 12.0f;
                text.HeightResizePolicy = ResizePolicyType.FillToParent;
                text.WidthResizePolicy = ResizePolicyType.FillToParent;
                Window.Add(text);
                return true;
            }

            public override void OnDestroy()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent2 OnDestroy");
                text.Dispose();
                animation.Dispose();
            }

            public override void OnPause()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent2 OnPause");
            }

            public override void OnResume()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent2 OnResume");
            }

            public override void OnStart(AppControl appControl, bool restarted)
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent2 OnStart");
            }

            public override void OnStop()
            {
                Tizen.Log.Error("MYLOG", "MyFrameComponent2 OnStop");
            }
        }
        static void Main(string[] args)
        {
            Dictionary<Type, string> dict = new Dictionary<Type, string>();
            dict.Add(typeof(MyFrameComponent), "csharp_frame");
            dict.Add(typeof(MyFrameComponent2), "csharp_frame2");
            var app = new Program(dict);

            app.Run(args);
        }
    }
}
