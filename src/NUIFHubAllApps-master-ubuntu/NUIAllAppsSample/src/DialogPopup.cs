using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIAllAppsSample
{
    public class DialogPopup : DialogPage
    {
        private DialogPage dialog;
        private TextLabel title;
        private TextLabel singer;

        public DialogPopup()
        {
            createPopup();
            Window.Instance.GetDefaultNavigator().TransitionFinished += ItemPopup_TransitionFinished;
        }

        private void ItemPopup_TransitionFinished(object sender, System.EventArgs e)
        {
            title.Show();
            singer.Show();
            Window.Instance.GetDefaultNavigator().TransitionFinished -= ItemPopup_TransitionFinished;
        }

        private void createPopup()
        {

            var view = new View()
            {
                Size = new Size(800, 1200),
                BackgroundColor = new Color(0.0f, 0.0f, 0.0f, 1.0f),
                CornerRadius = 0.1f,
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PositionUsesPivotPoint = true,

                TransitionOptions = new TransitionOptions()
                {
                    TransitionTag = "box",
                },
            };

            var albumIcon = new ImageView()
            {
                ResourceUrl = "./res/fountain.jpg",
                Size = new Size(500, 500),
                ParentOrigin = Tizen.NUI.ParentOrigin.Center,
                PivotPoint = Tizen.NUI.PivotPoint.Center,
                PositionUsesPivotPoint = true,
                Position = new Position(0, -150),
                CornerRadius = 0.15f,
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
                TransitionOptions = new TransitionOptions()
                {
                    TransitionTag = "album",
                },
            };
            view.Add(albumIcon);
            view.TouchEvent += Dialog_TouchEvent;

            title = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Together We Will Live Forever",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 0.8f),
                FontFamily = "Hey Comic",
                Position = new Position(0, 50),
            };
            title.Hide();
            view.Add(title);
            singer = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Clint Mansell - The Fountain OST",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 0.8f),
                FontFamily = "Hey Comic",
                Position = new Position(0, 120),
            };
            singer.Hide();
            view.Add(singer);

            Content = view;
        }

        private bool Dialog_TouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Window.Instance.GetDefaultNavigator().PopWithTransition();
            }
            return false;
        }
    }
}
