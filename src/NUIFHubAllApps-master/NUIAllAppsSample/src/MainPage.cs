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
                BackgroundImage = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/main.jpg",
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
                PointSize = 25,
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
                PointSize = 80,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "Friday",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 1.0f),
                Position = new Position(100, 950),
                FontFamily = "Hey Comic",
            };
            mainView.Add(Friday);

            var November = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.WrapContent,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                PointSize = 40,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "November",
                TextColor = new Color(0.98f, 0.98f, 0.98f, 1.0f),
                Position = new Position(100, 1080),
                FontFamily = "Hey Comic",
            };
            mainView.Add(November);

            var date = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.WrapContent,
                WidthSpecification = LayoutParamPolicies.WrapContent,
                PointSize = 50,
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                Text = "14th",
                TextColor = new Color("#b97a57"),
                Position = new Position(420, 1060),
                FontFamily = "Hey Comic",
            };
            mainView.Add(date);
        }

        private void CreateWidget()
        {
            var widgetView = new MusicWidgetView()
            {
                BackgroundColor = new Color(0.0f, 0.0f, 0.0f, 0.45f),
                Size = new Size(600, 80),
                CornerRadius = 0.2f,
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
                Position = new Position(450, 120),

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
                HeightSpecification = 800,
                Focusable = true,
                Position2D = new Position2D(0, 1920 - 650),
            };
            mainView.Add(mainApps);
        }
    }
}
