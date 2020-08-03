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
        public TextLabel MainText;
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

        private Position DefaultIconPos;
        private Position DefaultAddIconPos;

        private Position DefaultMainPos;
        private Position DefaultSubPos;
        private Position DefaultMainViewPos;

        private ImageView imgView;
        protected override void OnFrameResumed(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameResumed :" + frame.DirectionForward);
            base.OnFrameResumed(frame);
            imgView = frame.Image;

            StartAnimation();
            Animation ani = new Animation(300);
            ani.DefaultAlphaFunction = GetSineInOut80();

            DefaultIconPos = IconView.Position;
            DefaultAddIconPos = AddView.Position;
            DefaultMainPos = MainProfileText.Position;
            DefaultSubPos = SubProfileText.Position;
            DefaultMainViewPos = MainView.Position;
            //imgView.Size = new Size(600, 300);


            imgView.Size = new Size(470, 700);
            //imgView.SizeHeight = imgView.SizeHeight + 300;
            //imgView.SizeWidth = imgView.SizeWidth + 200;

            //imgView.Position = MainView.Position;
            imgView.ParentOrigin = ParentOrigin.TopCenter;
            imgView.PivotPoint = PivotPoint.TopCenter;
            imgView.PositionUsesPivotPoint = true;
            imgView.CornerRadius = 50.0f;
            if (frame.DirectionForward == true)
            {

                Tizen.Log.Error("MYLOG", "***Forward animation");

                ani.AnimateTo(IconView, "Position", new Position(0, 10), 0, 300);
                ani.AnimateTo(AddView, "Position", new Position(30, 40), 0, 300);
                ani.AnimateTo(AddView, "Scale", new Vector3(0.5f, 0.5f, 1.0f), 0, 300);

                ani.AnimateTo(MainProfileText, "Position", new Position(180, 60), 0, 300);
                ani.AnimateTo(SubProfileText, "Position", new Position(210, 90), 0, 300);

                ani.AnimateTo(MainView, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 0, 300);
                ani.AnimateTo(MainView, "Size", new Size(470, 700), 0, 300);

                ani.AnimateTo(Contents, "Opacity", 1.0f, 0, 300);



                Tizen.Log.Error("MYLOG", "Finish Animation");
                ani.Finished += Ani_Finished1;


                ani.Play();


            }
            else
            {
                Tizen.Log.Error("MYLOG", "***Backward animation");
            }

            StartAnimation();
        }


        protected override void OnFrameUpdated(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameUpdated");
            base.OnFrameUpdated(frame);
            //imgView.BackgroundColor = Color.White;
        }

        protected override void OnFramePaused()
        {
            base.OnFramePaused();
            //Finish();
        }
        private void Ani_Finished1(object sender, EventArgs e)
        {
            MainView.Add(imgView);
            IconView.RaiseToTop();
            AddView.RaiseToTop();

            Animation ani = new Animation(600);
            ani.DefaultAlphaFunction = GetSineInOut80();
            ani.AnimateTo(IconView, "Position", new Position(0, 150), 0, 300);
            ani.AnimateTo(AddView, "Position", new Position(30, 180), 0, 300);
            ani.AnimateTo(IconView, "Scale", new Vector3(1.2f, 1.2f, 1.0f), 0, 300);

            ani.AnimateTo(imgView, "Size", new Size(Window.Instance.WindowSize.Width, Window.Instance.WindowSize.Height), 0, 600);
            ani.AnimateTo(MainView, "Size", new Size(Window.Instance.WindowSize.Width, Window.Instance.WindowSize.Height), 0, 600);
            ani.AnimateTo(MainView, "Position", new Position(0, 0), 0, 600);
            ani.Finished += Ani_Finished;
            ani.Play();

            MainText.Hide();
            Contents.Opacity = 0.0f;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            FinishAnimation();
        }

        public void Finish()
        {
            MainText.Show();
            Tizen.Log.Error("MYLOG", "init ui objects");
            IconView.Position = DefaultIconPos;
            IconView.Scale = new Vector3(1.0f, 1.0f, 1.0f);
            AddView.Scale = new Vector3(1.0f, 1.0f, 1.0f);
            AddView.Position = DefaultAddIconPos;


            MainProfileText.Position = DefaultMainPos;
            SubProfileText.Position = DefaultSubPos;
            MainView.Position = DefaultMainViewPos;

            MainView.Size = new Size(470, 600);


            imgView.Unparent();
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
