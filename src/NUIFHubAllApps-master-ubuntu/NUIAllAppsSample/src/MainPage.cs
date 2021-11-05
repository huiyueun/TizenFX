using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIAllAppsSample
{
    public class MainPage : ContentPage
    {
        private ImageView mainView;
        public MainPage()
        {
            //main view
            mainView = new ImageView()
            {
                Name = "Default Main View",
                Layout = new AbsoluteLayout() { },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                BackgroundImage = "./res/main.jpg",
            };

            Content = mainView;

            CreateTextLabel();
            CreateWidget();
            CreateApps();
        }

        private void CreateTextLabel()
        {
            var hello = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.WrapContent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "Good Morning, huiyu",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 1.0f),
                Position = new Position(0, 50),
                FontFamily = "Hey Comic",
            };
            mainView.Add(hello);


            var Friday = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.WrapContent,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                PointSize = 35,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Friday",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 1.0f),
                Position = new Position(40, 430),
                FontFamily = "Hey Comic",
            };
            mainView.Add(Friday);

            var November = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.WrapContent,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                PointSize = 14,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "November",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 1.0f),
                Position = new Position(40, 485),
                FontFamily = "Hey Comic",
            };
            mainView.Add(November);

            var date = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.WrapContent,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                PointSize = 18,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "14th",
                TextColor = new Color("#b97a57"),
                Position = new Position(175, 475),
                FontFamily = "Hey Comic",
            };
            mainView.Add(date);
        }

        private void CreateWidget()
        {
            var widgetView = new MusicWidgetView()
            {
                BackgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.45f),
                Size = new Size(380, 60),
                CornerRadius = 0.2f,
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
                Position = new Position(75, 120),

                TransitionOptions = new TransitionOptions()
                {
                    TransitionTag = "box",
                },
            };
            mainView.Add(widgetView);
        }

        private void CreateApps()
        {
            var mainApps = new AppsView()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    LinearAlignment = LinearLayout.Alignment.Top,
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = 400,
                Focusable = true,
                Position2D = new Position2D(0, 550),
            };
            mainView.Add(mainApps);
        }
    }
}
