using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIAllAppsSample
{
    public class MusicWidgetView : View
    {

        private Position downPosition = new Position(0, 0);
        private Position movePosition = new Position(0, 0);

        public MusicWidgetView()
        {
            this.TouchEvent += Widget_TouchEvent;
            CreateWidget();
        }

        private void CreateWidget()
        {
            var albumIcon = new ImageView()
            {
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/fountain.jpg",
                Size = new Size(60, 60),
                Position = new Position(20, 10),
                CornerRadius = 0.15f,
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
                TransitionOptions = new TransitionOptions()
                {
                    TransitionTag = "album",
                },
            };
            this.Add(albumIcon);
            var title = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 15,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Together We Will Live Forever",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 0.8f),
                FontFamily = "Hey Comic",
                Position = new Position(110, 15),
            };
            this.Add(title);
            var singer = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 13,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Clint Mansell - The Fountain OST",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 0.8f),
                FontFamily = "Hey Comic",
                Position = new Position(110, 45),
            };
            this.Add(singer);
            var play = new ImageView()
            {
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/previous.png",
                Size = new Size(25, 25),
                Position = new Position(460, 27),
                Color = Color.White,
            };
            this.Add(play);
            var prev = new ImageView()
            {
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/play.png",
                Size = new Size(35, 35),
                Position = new Position(500, 22),
                Color = Color.White,
            };
            this.Add(prev);
            var next = new ImageView()
            {
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/next-button.png",
                Size = new Size(33, 33),
                Position = new Position(540, 23),
                Color = Color.White,
            };
            this.Add(next);
        }

        private bool Widget_TouchEvent(object source, View.TouchEventArgs e)
        {
            PointStateType type = e.Touch.GetState(0);
            Vector2 vector = e.Touch.GetScreenPosition(0);
            var view = source as View;

            switch (type)
            {
                case PointStateType.Down:
                    downPosition = movePosition = new Position(vector);
                    PlayScaleDownAnimation(view);
                    break;
                case PointStateType.Motion:
                    var currentPosition = new Position(vector);
                    view.Position = new Position(view.Position) + (currentPosition - movePosition);
                    movePosition = currentPosition;
                    break;
                case PointStateType.Up:
                    if (isTouchArea(AbsPosition(new Position(vector) - downPosition)))
                    {
                        ShowPopup();
                    }

                    PlayScaleUpAnimation(view);
                    break;
            }
            return false;
        }


        private void ShowPopup()
        {
            Window.Instance.GetDefaultNavigator().PushWithTransition(new DialogPopup());
        }

        private void PlayScaleDownAnimation(View view)
        {
            //ClearAnimation();
            Animation startAni = new Animation(150);
            startAni.AnimateTo(view, "Scale", new Vector3(0.9f, 0.9f, 1.0f));
            startAni.Play();
        }

        private void PlayScaleUpAnimation(View view)
        {
            //ClearAnimation();
            Animation startAni = new Animation(150);
            startAni.AnimateTo(view, "Scale", new Vector3(1.0f, 1.0f, 1.0f));
            startAni.Play();
        }

        private Position AbsPosition(Position2D position)
        {
            return new Position(Math.Abs(position.X), Math.Abs(position.Y));
        }

        private bool isTouchArea(Position2D position)
        {
            return (position.X < 5 && position.Y < 5);
        }
    }
}
