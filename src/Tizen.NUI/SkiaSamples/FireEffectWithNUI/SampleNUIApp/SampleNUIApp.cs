using System;
using System.Collections.Generic;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace SampleNUIApp
{
    class Program : NUIApplication
    {
        public List<Circle> Circles = new List<Circle>();
        private Window WindowInstance;
        private View circlesView;
        private FrameBuffer frameBuffer;

        private uint sizeWidth;
        private uint sizeHeight;

        protected override void OnCreate()
        {
            base.OnCreate();
            InitDefaultOptions();
            Initialize();
        }

        private void InitDefaultOptions()
        {
            WindowInstance = Window.Instance;
            WindowInstance.BackgroundColor = Color.Black;

            WindowInstance.KeyEvent += OnKeyEvent;

            sizeWidth = 200;// (uint)WindowInstance.Size.Width;
            sizeHeight = 200;// (uint)WindowInstance.Size.Height;
            Tizen.Log.Error("MYLOG", $"size : {sizeWidth}, {sizeHeight}");
        }

        private void Initialize()
        {
            var effectView = new ShaderEffectView(new Size2D((int)sizeWidth, (int)sizeHeight), new Position2D(0, 0));
            effectView.SetRenderer(null);
            effectView.StartDrawing();
            //var mainTextureSet = new TextureSet();
            //var renderTask = CreateRenderTaskByView(effectView);
            //mainTextureSet.SetTexture(0, renderTask.GetFrameBuffer().GetColorTexture());





            WindowInstance.Add(effectView);
            //var renderTask = CreateRenderTaskByView(blurView);
            //var effectView = new ShaderEffectView(new Size2D((int)sizeWidth, (int)sizeHeight), new Position2D(0, 0), mainTextureSet);
        }

        private bool OnTimerTick(object source, Timer.TickEventArgs e)
        {
            if (new Random().NextDouble() > .18)
            {
                var circle = new Circle(sizeWidth / 2, sizeHeight / 2, 1 + new Random().NextDouble());
                Circles.Add(circle);
                //circlesView.Add(circle);
                for (var i = 0; i < Circles.Count; i++)
                {
                    Circles[i].Update(circlesView, sizeWidth / 2, sizeHeight / 2, Circles);
                }
            }
            return true;
        }

        private void OnKeyEvent(object sender, Window.KeyEventArgs e)
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

        ///Example :: RenderTask
        private void SetCirclesRenderTask()
        {
            //Blur View Example
            var blurView = new GaussianBlurView(10, 3.0f, PixelFormat.RGBA8888, 0.5f, 0.5f, false)
            {
                Size = new Size(sizeWidth, sizeHeight),
                Position = new Position(0, 0),
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                BackgroundColor = Color.White,
            };
            circlesView = new View()
            {
                Size = new Size(sizeWidth, sizeHeight),
                Position = new Position(0, 0),

                BackgroundColor = Color.Transparent,
            };
            //blurView.Add(circlesView);
            //blurView.Activate();
            //WindowInstance.Add(circlesView);
        }

        private RenderTask CreateRenderTaskByView(View view)
        {
            var renderTask = Window.Instance.GetRenderTaskList().CreateTask();
            renderTask.SetSourceView(view);
            renderTask.SetExclusive(false);

            var texture = new Texture(TextureType.TEXTURE_2D, PixelFormat.RGBA8888, sizeWidth, sizeHeight);

            frameBuffer = new FrameBuffer(sizeWidth, sizeHeight, 3);
            frameBuffer.AttachColorTexture(texture);
            renderTask.SetFrameBuffer(frameBuffer);
            renderTask.SetClearEnabled(true);

            return renderTask;
        }
    }
}
