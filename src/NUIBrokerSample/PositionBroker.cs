using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
{
    class PositionBroker : FrameBrokerBase
    {
        private Window window;
        public View IconView;
        public View AddView;
        public TextLabel MainProfileText;
        public TextLabel SubProfileText;
        public TextLabel Contents;

        public View MainView;

        public PositionBroker(Window window) : base(window)
        {
            this.window = window;
        }

        protected override void OnFrameCreated()
        {
            base.OnFrameCreated();
        }

        protected override void OnFrameDestroyed()
        {
            base.OnFrameDestroyed();
        }

        protected override void OnFramePaused()
        {
            base.OnFramePaused();
        }
        private Position DefaultIconPos;
        private Position DefaultAddIconPos;

        private Position DefaultMainPos;
        private Position DefaultSubPos;

        protected override void OnFrameResumed(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameResumed :" + frame.DirectionForward);
            base.OnFrameResumed(frame);
            //imgView = frame.Image;
            //window.Add(imgView);
            StartAnimation();
            Animation ani = new Animation(330);
            ani.DefaultAlphaFunction = GetSineInOut80();

            DefaultIconPos = IconView.Position;
            DefaultAddIconPos = AddView.Position;
            DefaultMainPos = MainProfileText.Position;
            DefaultSubPos = SubProfileText.Position;
            if (frame.DirectionForward == true)
            {
                Tizen.Log.Error("MYLOG", "***Forward animation");


                ani.AnimateTo(IconView, "Position", new Position(0, 10));
                ani.AnimateTo(AddView, "Position", new Position(30, 40));
                ani.AnimateTo(AddView, "Scale", new Vector3(0.5f, 0.5f, 1.0f));

                ani.AnimateTo(MainProfileText, "Position", new Position(180, 60));
                ani.AnimateTo(SubProfileText, "Position", new Position(210, 90));

                ani.AnimateTo(MainView, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                ani.AnimateTo(MainView, "Size", new Size(470, 700));

                ani.AnimateTo(Contents, "Opacity", 1.0f);

            }
            else 
            {
                Tizen.Log.Error("MYLOG", "***Backward animation");

                //ani.AnimateTo(imgView, "Position", new Position(0, -600));
                //ani.AnimateTo(imgView, "Size", new Size(0, 0));
            }

            ani.Finished += Ani_Finished;
            ani.Play();
            StartAnimation();
        }

        protected override void OnFrameUpdated(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameUpdated");
            base.OnFrameUpdated(frame);

            //imgView.BackgroundColor = Color.White;

            Tizen.Log.Error("MYLOG", "Start Animation");
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            Tizen.Log.Error("MYLOG", "Finish Animation");
            Animation ani = new Animation(100);
            ani.DefaultAlphaFunction = GetSineInOut80();
            ani.AnimateTo(MainView, "Size", new Size(Window.Instance.WindowSize.Width, Window.Instance.WindowSize.Height));
            ani.AnimateTo(MainView, "Position", new Position(0, 0));
            ani.Finished += Ani_Finished1;
            ani.Play();

            //finish();
            /*
            Timer timer = new Timer(500);
            timer.Tick += Timer_Tick;
            timer.Start();*/

            //            imgView.Unparent();
            //imgView.Dispose();
            //imgView = null;
        }

        private void Ani_Finished1(object sender, EventArgs e)
        {
            FinishAnimation();
        }

        public void Finish()
        {
            IconView.Position = DefaultIconPos;
            AddView.Position = DefaultAddIconPos;

            MainProfileText.Position = DefaultMainPos;
            SubProfileText.Position = DefaultSubPos;

            MainView.Size = new Size(470, 600);

            AddView.Scale = new Vector3(1.0f, 1.0f, 1.0f);

            Contents.Opacity = 0.0f;
        }

        private bool Timer_Tick(object source, Timer.TickEventArgs e)
        {
            Finish();
            return false;
        }


        /// <summary>
        /// GlideOut, 0.25, 0.46, 0.45, 1.0
        /// </summary>
        internal AlphaFunction GetGlideOut()
        {
            return new AlphaFunction(new Vector2(0.25f, 0.46f), new Vector2(0.45f, 1.0f));
        }

        /// <summary>
        /// SineInOut80, 0.33, 0.0, 0.2, 1.0
        /// </summary>
        internal AlphaFunction GetSineInOut80()
        {
            return new AlphaFunction(new Vector2(0.99f, 0.0f), new Vector2(0.58f, 1.0f));
        }

        /// <summary>
        /// GetSineOut33, 0.17, 0.17, 0.67, 1.0
        /// </summary>
        internal AlphaFunction GetSineOut33()
        {
            return new AlphaFunction(new Vector2(0.57f, 0.17f), new Vector2(0.17f, 1.0f));
        }

        internal AlphaFunction GetEaseOutSquare()
        {
            return new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare);
        }
    }
}
