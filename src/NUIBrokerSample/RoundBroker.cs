using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
{
    class RoundBroker : FrameBrokerBase
    {
        private Window window;
        private ImageView imgView;
        public ImageView sharedResource;

        
        public RoundBroker(Window window) : base(window)
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
            window.Add(imgView);
            imgView.ParentOrigin = ParentOrigin.Center;
            imgView.PivotPoint = PivotPoint.Center;
            imgView.PositionUsesPivotPoint = true;
            imgView.Size = new Size(360, 360);

            Animation ani = new Animation(70);
            ani.DefaultAlphaFunction = GetSineOut33();
            ani.Finished += Ani_Finished;

            Animation shared = new Animation(100);
            shared.Finished += Shared_Finished;

            if (frame.DirectionForward == true)
            { 
                Tizen.Log.Error("MYLOG", "***Forward animation");
                imgView.Position = new Position(0, 100);
                imgView.Scale = new Vector3(0.5f, 0.5f, 0.5f);

                ani.AnimateTo(imgView, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
                ani.AnimateTo(imgView, "Position", new Position(0, 0));

                shared.AnimateTo(sharedResource, "Position", new Position(0, -100));
            }
            else
            {
                Tizen.Log.Error("MYLOG", "***Backward animation");
                imgView.Position = new Position(0, 0);
                imgView.Scale = new Vector3(1.0f, 1.0f, 1.0f);

                ani.AnimateTo(imgView, "Scale", new Vector3(0.5f, 0.5f, 0.5f));
                ani.AnimateTo(imgView, "Position", new Position(0, 100));

                shared.AnimateTo(sharedResource, "Position", new Position(0, 100));
            }

            ani.Play();
            shared.Play();


            Tizen.Log.Error("MYLOG", "Start Animation");
            StartAnimation();
        }

        private void Shared_Finished(object sender, EventArgs e)
        {
            //sharedResource.Position = new Position(0, 100);
        }

        protected override void OnFrameUpdated(FrameData frame)
        {
            Tizen.Log.Error("MYLOG", "OnFrameUpdated");
            base.OnFrameUpdated(frame);

            //imgView.BackgroundColor = Color.White;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            Tizen.Log.Error("MYLOG", "Finish Animation");
            FinishAnimation();
            imgView.Unparent();
            imgView.Dispose();
            //imgView = null;
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
            return new AlphaFunction(new Vector2(0.33f, 0.0f), new Vector2(0.2f, 1.0f));
        }

        /// <summary>
        /// GetSineOut33, 0.17, 0.17, 0.67, 1.0
        /// </summary>
        internal AlphaFunction GetSineOut33()
        {
            return new AlphaFunction(new Vector2(0.17f, 0.17f), new Vector2(0.67f, 1.0f));
        }

        internal AlphaFunction GetEaseOutSquare()
        {
            return new AlphaFunction(AlphaFunction.BuiltinFunctions.EaseOutSquare);
        }
    }
}
