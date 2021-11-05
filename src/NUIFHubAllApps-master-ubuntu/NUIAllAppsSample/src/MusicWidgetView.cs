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
                ResourceUrl = "./res/fountain.jpg",
                Size = new Size(40, 40),
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
                PointSize = 10,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Together We Will Live Forever",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 0.8f),
                FontFamily = "Hey Comic",
                Position = new Position(80, 13),
            };
            this.Add(title);
            var singer = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 8,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Clint Mansell - The Fountain OST",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 0.8f),
                FontFamily = "Hey Comic",
                Position = new Position(80, 37),
            };
            this.Add(singer);
            var play = new ImageView()
            {
                ResourceUrl = "./res/previous.png",
                Size = new Size(15, 15),
                Position = new Position(310, 23),
                Color = Color.White,
            };
            this.Add(play);
            var prev = new ImageView()
            {
                ResourceUrl = "./res/play.png",
                Size = new Size(22, 22),
                Position = new Position(330, 20),
                Color = Color.White,
            };
            this.Add(prev);
            var next = new ImageView()
            {
                ResourceUrl = "./res/next-button.png",
                Size = new Size(20, 20),
                Position = new Position(350, 21),
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
