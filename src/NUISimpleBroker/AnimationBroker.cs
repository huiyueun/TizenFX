using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUISimpleBroker
{
    public class AnimationBroker : FrameBrokerBase
    {
        private Window window;
        private ImageView imgView;

        public AnimationBroker(Window window) : base(window)
        {
            this.window = window;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnFrameCreated()
        {
            base.OnFrameCreated();
        }

        protected override void OnFrameDestroyed()
        {
            base.OnFrameDestroyed();
        }

        protected override void OnFrameErred(FrameError error)
        {
            base.OnFrameErred(error);
            Tizen.Log.Error("MYLOG", "err : " + error);
        }

        protected override void OnFramePaused()
        {
            Tizen.Log.Error("MYLOG", "Frame paused");
            base.OnFramePaused();

        }

        protected override void OnFrameResumed(FrameData frame)
        {
            base.OnFrameResumed(frame);
            Animation ani = new Animation(700);

            Tizen.Log.Error("MYLOG", "Start Animation :" + frame.DirectionForward);

            imgView = frame.Image; ;
            imgView.ParentOrigin = ParentOrigin.Center;
            imgView.PivotPoint = PivotPoint.Center;
            imgView.PositionUsesPivotPoint = true;
            imgView.Size = new Size(360, 360);

            Window.Instance.Add(imgView);

            if (frame.DirectionForward == true)
            {
                imgView.Position = new Position(360, 0);
                ani.AnimateTo(imgView, "Position", new Position(0, 0));
            }
            else
            {
                imgView.Position = new Position(0, 0);
                ani.AnimateTo(imgView, "Size", new Size(360, 0));
            }
            StartAnimation();
            ani.Finished += Ani_Finished1;
            ani.Play();
            Tizen.Log.Error("MYLOG", "Start Animation");
        }

        private void Ani_Finished1(object sender, EventArgs e)
        {
            FinishAnimation();
            Tizen.Log.Error("MYLOG", "Finish Animation");
            imgView.Unparent();
            imgView.Dispose();
            imgView = null;
        }

        protected override void OnFrameUpdated(FrameData frame)
        {
            base.OnFrameUpdated(frame);
        }
    }
}
