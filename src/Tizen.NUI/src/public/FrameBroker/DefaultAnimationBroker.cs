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

        protected override void OnFrameResumed(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameResumed");
            base.OnFrameResumed(frame);
            imgView = frame.Image;
            imgView.Size = window.Size;
            window.Add(imgView);
            imgView.Opacity = 0.0f;

            Animation ani = new Animation(500);
            ani.AnimateTo(imgView, "Opacity", 1.0f);
            ani.Play();
            ani.Finished += Ani_Finished;
            StartAnimation();
        }
        
        private void Ani_Finished(object sender, EventArgs e)
        {
            Tizen.Log.Error("MYLOG", "Finish Animation");
            FinishAnimation();
            imgView.Unparent();
            imgView.Dispose();
            imgView = null;
        }
    }
}
