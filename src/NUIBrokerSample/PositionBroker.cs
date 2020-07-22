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
        private ImageView imgView;

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

        protected override void OnFrameResumed(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameResumed :" + frame.DirectionForward);
            base.OnFrameResumed(frame);
            imgView = frame.Image;
            window.Add(imgView);

            Animation ani = new Animation(500);
            if (frame.DirectionForward == true)
            {
                Tizen.Log.Error("MYLOG", "***Forward animation");
                imgView.Size = new Size(0, 0);
                imgView.Position = new Position(0, -600);

                ani.AnimateTo(imgView, "Position", new Position(0, 0));
                ani.AnimateTo(imgView, "Size", new Size(360, 360));

            }
            else 
            {
                Tizen.Log.Error("MYLOG", "***Backward animation");
                imgView.Size = new Size(360, 360);
                imgView.Position = new Position(0, 0);

                ani.AnimateTo(imgView, "Position", new Position(0, -600));
                ani.AnimateTo(imgView, "Size", new Size(0, 0));
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
            FinishAnimation();
            imgView.Unparent();
            imgView.Dispose();
            //imgView = null;
        }
    }
}
