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

        private Position DefaultIconPos;
        private Position DefaultAddIconPos;
        private Position DefaultMainPos;
        private Position DefaultSubPos;
        private Position DefaultMainViewPos;

        private ImageView imgView;

        public PositionBroker(Window window) : base(window)
        {
            this.window = window;
        }

        protected override void OnFrameCreated()
        {
        }

        protected override void OnFrameDestroyed()
        {
        }

        protected override void OnFrameResumed(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameResumed :" + frame.DirectionForward);


            Animation ani = new Animation(100);
            //ani.DefaultAlphaFunction = GetSineInOut80();

            if (frame.DirectionForward == true)
            {

                DefaultIconPos = IconView.Position;
                DefaultAddIconPos = AddView.Position;
                DefaultMainPos = MainProfileText.Position;
                DefaultSubPos = SubProfileText.Position;
                DefaultMainViewPos = MainView.Position;

                imgView = frame.Image;
                imgView.Size = new Size(470, 700);
                imgView.ParentOrigin = ParentOrigin.TopCenter;
                imgView.PivotPoint = PivotPoint.TopCenter;
                imgView.PositionUsesPivotPoint = true;
                imgView.CornerRadius = 50.0f;


                ani.AnimateTo(IconView, "Position", new Position(0, 10), 0, 300);
                ani.AnimateTo(AddView, "Position", new Position(30, 40), 0, 300);
                ani.AnimateTo(AddView, "Scale", new Vector3(0.5f, 0.5f, 1.0f), 0, 300);

                ani.AnimateTo(MainProfileText, "Position", new Position(180, 60), 0, 300);
                ani.AnimateTo(SubProfileText, "Position", new Position(210, 90), 0, 300);

                ani.AnimateTo(MainView, "Scale", new Vector3(1.0f, 1.0f, 1.0f), 0, 300);
                ani.AnimateTo(MainView, "Size", new Size(470, 700), 0, 300);

                ani.AnimateTo(Contents, "Opacity", 1.0f, 0, 300);
                ani.Finished += Ani_Finished1;
            }
            else
            {
                //imgView.Size = new Size(1080, 1920);
                //MainView.Add(imgView);

                MainProfileText.Opacity = 1.0f;
                SubProfileText.Opacity = 1.0f;
                MainText.Opacity = 1.0f;
                Contents.Opacity = 1.0f;

                ani.AnimateTo(imgView, "Size", new Size(470, 600));
                ani.AnimateTo(MainView, "Size", new Size(470, 600));
                ani.AnimateTo(MainView, "Position", DefaultMainViewPos);

                ani.AnimateTo(IconView, "Position", DefaultIconPos);
                ani.AnimateTo(IconView, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                ani.AnimateTo(AddView, "Position", DefaultAddIconPos);
                ani.AnimateTo(MainProfileText, "Position", DefaultMainPos);
                ani.AnimateTo(SubProfileText, "Position", DefaultSubPos);
                ani.AnimateTo(MainText, "Opacity", 1.0f);
                ani.AnimateTo(Contents, "Opacity", 1.0f);
                ani.Finished += Ani_Finished2;
            }

            StartAnimation();
            ani.Play();
        }


        protected override void OnFrameUpdated(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameUpdated");
            //imgView.BackgroundColor = Color.White;
        }

        protected override void OnFramePaused()
        {
            Tizen.Log.Error("MYLOG", "OnFramePaused");
        }

        private void Ani_Finished2(object sender, EventArgs e)
        {
            FinishAnimation();
            DeleteImage();
        }

        public void DeleteImage()
        {

            imgView.Unparent();
            imgView.Dispose();
            imgView = null;
        }

        private void Ani_Finished1(object sender, EventArgs e)
        {
            MainView.Add(imgView);
            IconView.RaiseToTop();
            AddView.RaiseToTop();

            Animation ani = new Animation(200);
            ani.DefaultAlphaFunction = GetSineInOut80();
            ani.AnimateTo(IconView, "Position", new Position(0, 150), 0, 300);
            ani.AnimateTo(IconView, "Scale", new Vector3(1.2f, 1.2f, 1.0f), 0, 300);
            ani.AnimateTo(AddView, "Position", new Position(30, 180), 0, 300);

            ani.AnimateTo(imgView, "Size", new Size(Window.Instance.WindowSize.Width, Window.Instance.WindowSize.Height), 0, 600);
            ani.AnimateTo(MainView, "Size", new Size(Window.Instance.WindowSize.Width, Window.Instance.WindowSize.Height), 0, 600);
            ani.AnimateTo(MainView, "Position", new Position(0, 0), 0, 600);

            MainProfileText.Opacity = 0.0f;
            SubProfileText.Opacity = 0.0f;
            MainText.Opacity = 0.0f;
            Contents.Opacity = 0.0f;
            ani.Finished += Ani_Finished;
            ani.Play();
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            FinishAnimation();
        }

        internal AlphaFunction GetSineInOut80()
        {
            return new AlphaFunction(new Vector2(0.45f, 0.43f), new Vector2(0.41f, 1.0f));
        }

    }
}
