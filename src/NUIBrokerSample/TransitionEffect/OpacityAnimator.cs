using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
{
    class OpacityAnimator : FrameBrokerBase
    {
        private Window window;
        private ImageView imgView;

        public OpacityAnimator(Window window) : base(window)
        {
            this.window = window;
        }

        protected override void OnFrameCreated()
        {
        }

        protected override void OnFrameDestroyed()
        {
        }

        protected override void OnFrameErred(FrameError error)
        {
        }

        protected override void OnFramePaused()
        {
        }

        protected override void OnFrameResumed(FrameData frame)
        {
            if(imgView?.GetParent() != null)
            {
                imgView.Unparent();
            }
            imgView = frame.Image;
            imgView.Size = window.Size;
            window.Add(imgView);

            Animation ani = new Animation(500);
            if (frame.DirectionForward)
            {
                imgView.Opacity = 0.0f;
                ani.AnimateTo(imgView, "Opacity", 1.0f);
            }
            else
            {
                imgView.Opacity = 1.0f;
                ani.AnimateTo(imgView, "Opacity", 0.0f);
            }

            ani.Play();
            ani.Finished += Ani_Finished;
            StartAnimation();
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            FinishAnimation();
        }

        protected override void OnFrameUpdated(FrameData frame)
        {
        }
    }
}
