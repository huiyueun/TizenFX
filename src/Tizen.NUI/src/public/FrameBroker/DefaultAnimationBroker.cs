using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.BaseComponents;

namespace Tizen.NUI
{
    public class DefaultAnimationBroker : FrameBrokerBase
    {
        private Window window;
        private ImageView imgView;

        public DefaultAnimationBroker(Window window) : base(window)
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
            Tizen.Log.Error("MYLOG", "OnFrameResumed");
            base.OnFrameResumed(frame);
            imgView = frame.Image;
            imgView.Size = new Size(360, 360);
            window.Add(imgView);
            imgView.Opacity = 0.0f;


            Animation ani = new Animation(500);
            ani.AnimateTo(imgView, "Opacity", 1.0f);
            ani.Play();
            ani.Finished += Ani_Finished;
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
