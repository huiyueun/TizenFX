using System;
using Tizen.Applications;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace NUIBrokerSample
{
    class Program : NUIApplication
    {
        private Color MAIN_TEXT_COLOR = new Color(0.95f, 0.95f, 0.95f, 1.0f);
        private Color SUB_TEXT_COLOR = new Color(0.70f, 0.70f, 0.70f, 1.0f);

        private float MAIN_TEXT_POINT_SIZE = 32.0f;
        private float PROFILE_TEXT_POINT_SIZE = 14.0f;
        private float SUB_TEXT_POINT_SIZE = 11.0f;

        private Color INFO_TEXT_COLOR = new Color(0.3f, 0.3f, 0.3f, 1.0f);
        private float INFO_TEXT_POINT_SIZE = 13.0f;

        private Color CONTENTS_TEXT_COLOR = new Color(0.6f, 0.6f, 0.6f, 1.0f);
        private float CONTENTS_TEXT_POINT_SIZE = 12.0f;


        private View mainView;
        private PositionBroker lauchBroker;

        private View profileContainer;
        private View add_container;

        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        void Initialize()
        {

            Window.Instance.KeyEvent += OnKeyEvent;
            Window.Instance.BackgroundColor = new Color(0.9f, 0.9f, 0.9f, 1.0f);

            lauchBroker = new PositionBroker(Window.Instance);

            ImageView bgView = new ImageView()
            {
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/familyboard_setting_bg1.png",
                Size = new Size(Window.Instance.WindowSize.Width, Window.Instance.WindowSize.Height),
            };
            Window.Instance.Add(bgView);
            mainView = new View()
            {
                Size = new Size(470, 600),
                //Size = new Size(50, 50),
                BackgroundColor = Color.Black,
                CornerRadius = 10.0f,
                ParentOrigin = ParentOrigin.TopCenter,
                PivotPoint = PivotPoint.TopCenter,
                Position = new Position(-50, 500),
                PositionUsesPivotPoint = true,
            };
            lauchBroker.MainView = mainView;



            View view = mainView;
            Window.Instance.Add(view);
            view.TouchEvent += View_TouchEvent; ;

            
            ImageView imgView = new ImageView()
            {
                ParentOrigin = ParentOrigin.BottomCenter,
                PivotPoint = PivotPoint.BottomCenter,
                PositionUsesPivotPoint = true,
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/pic_1.jpg",
                //Position = new Position(0, -30, 0),
                Size = new Size(360, 360),
            };
            view.Add(imgView);

            TextLabel contents = new TextLabel()
            {
                Text = "Beautiful, dreamy and dramtic\n instrumental neo classical piano scores\n from movies and tv series.\n",
                MultiLine = true,
                TextColor = CONTENTS_TEXT_COLOR,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                PointSize = CONTENTS_TEXT_POINT_SIZE,
                Position = new Position(0, 250, 0),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                Opacity = 0.0f,
            };
            view.Add(contents);
            lauchBroker.Contents = contents;

            CreateTopProfile(view);

            PropertyMap map = new PropertyMap();
            map.Insert("weight", new PropertyValue("bold"));

            TextLabel text = new TextLabel("Cinematic Piano")
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                TextColor = MAIN_TEXT_COLOR,
                PointSize = MAIN_TEXT_POINT_SIZE,
                Position = new Position(0, 150, 0),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                FontStyle = map,
            };
            view.Add(text);
            lauchBroker.MainText = text;

            CreateInfo(view);

            View play_btn = new View()
            {
                ParentOrigin = ParentOrigin.BottomCenter,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                BackgroundColor = Color.White,
                Position = new Position(0, -40),
                Size = new Size(52, 52),
                CornerRadius = 26.0f,
            };


            ImageView play_icon = new ImageView()
            {
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/play-128.png",
                Color = Color.Black,
                Size = new Size(20, 20),
            };
            play_btn.Add(play_icon);
            view.Add(play_btn);
            lauchBroker.IconView = profileContainer;
            lauchBroker.AddView = add_container;
            
            
            Window.Instance.SetFramerBroker(lauchBroker);
        }

        private bool View_TouchEvent2(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                launchApplication();
            }
            return false;
        }


        protected override void OnPause()
        {
            base.OnPause();
            lauchBroker.Finish();
        }

        public void CreateTopProfile(View view)
        {
            profileContainer = new View()
            {
                ParentOrigin = ParentOrigin.TopCenter,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                BackgroundColor = Color.White,
                Position = new Position(-160, 80, 0),
                Size = new Size(76, 76, 0),
                CornerRadius = 38.0f,
            };

            View profileContainer_inner = new View()
            {
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                BackgroundColor = Color.Black,
                Size = new Size(74, 74, 0),
                CornerRadius = 37.0f,
            };

            ImageView profileImage = new ImageView()
            {
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/profile.jpg",
                Size = new Size(62, 62, 0),
                CornerRadius = 31.0f,
            };
            profileContainer.Add(profileContainer_inner);
            profileContainer_inner.Add(profileImage);
            view.Add(profileContainer);

            PropertyMap map = new PropertyMap();
            map.Insert("weight", new PropertyValue("bold"));
            TextLabel profile_text1 = new TextLabel("PIANO DAILY")
            {
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                TextColor = MAIN_TEXT_COLOR,
                PointSize = PROFILE_TEXT_POINT_SIZE,
                Position = new Position(125, 60, 0),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
                FontStyle = map,
            };
            view.Add(profile_text1);

            TextLabel profile_text2 = new TextLabel("July 2020")
            {
                HorizontalAlignment = HorizontalAlignment.Begin,
                VerticalAlignment = VerticalAlignment.Top,
                TextColor = SUB_TEXT_COLOR,
                PointSize = SUB_TEXT_POINT_SIZE,
                Position = new Position(125, 90, 0),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
            };
            view.Add(profile_text2);

            lauchBroker.MainProfileText = profile_text1;
            lauchBroker.SubProfileText = profile_text2;


            add_container = new View()
            {
                ParentOrigin = ParentOrigin.TopCenter,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                BackgroundColor = Color.White,
                Position = new Position(160, 80),
                Size = new Size(60, 60),
                CornerRadius = 30.0f,
            };
            ImageView add_icon = new ImageView()
            {
                ParentOrigin = ParentOrigin.Center,
                PivotPoint = PivotPoint.Center,
                PositionUsesPivotPoint = true,
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/add.png",
                Size = new Size(15, 15),
            };
            add_container.Add(add_icon);
            view.Add(add_container);
        }

        public void CreateInfo(View view)
        {
            int posY = 210;
            TextLabel info1 = new TextLabel()
            {
                Text = "9,465\n",
                TextColor = INFO_TEXT_COLOR,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                PointSize = INFO_TEXT_POINT_SIZE,
                Position = new Position(-35, posY, 0),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
            };
            view.Add(info1);
            ImageView info1_icon = new ImageView()
            {
                ParentOrigin = ParentOrigin.TopCenter,
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/bar-chart-5-128.png",
                Color = INFO_TEXT_COLOR,
                Position = new Position(-85, posY, 0),
                Size = new Size(15, 15),
            };
            view.Add(info1_icon);


            TextLabel info2 = new TextLabel()
            {
                Text = "5h 35m\n",
                TextColor = INFO_TEXT_COLOR,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                PointSize = INFO_TEXT_POINT_SIZE,
                Position = new Position(65, posY, 0),
                HeightResizePolicy = ResizePolicyType.FillToParent,
                WidthResizePolicy = ResizePolicyType.FillToParent,
            };
            view.Add(info2);

            ImageView info2_icon = new ImageView()
            {
                ParentOrigin = ParentOrigin.TopCenter,
                ResourceUrl = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "images/clock-128.png",
                Color = INFO_TEXT_COLOR,
                Position = new Position(10, posY, 0),
                Size = new Size(15, 15),
            };
            //view.Add(info2_icon);

        }

        private Vector2 prePos = new Vector2(0, 0);
        private Vector2 firstPos = new Vector2(0, 0);
        private bool isMoving = false;
        private bool View_TouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Down)
            {
                prePos = e.Touch.GetScreenPosition(0);
                firstPos = prePos;
            }
            else if (e.Touch.GetState(0) == PointStateType.Motion)
            {
                Vector2 curPos = e.Touch.GetScreenPosition(0);
                float moveX = curPos.X - prePos.X;
                float moveY = curPos.Y - prePos.Y;
                isMoving = true;
                Position mPos = mainView.Position;
                mainView.Position = new Position(mPos.X + moveX, mPos.Y + moveY);

                prePos = curPos;
            }
            else if (e.Touch.GetState(0) == PointStateType.Up)
            {
                Vector2 curPos = e.Touch.GetScreenPosition(0);
                float moveX = Math.Abs(curPos.X - firstPos.X);
                float moveY = Math.Abs(curPos.Y - firstPos.Y);

                if (moveX < 5 && moveY < 5)
                {
                    Tizen.Log.Error("MYLOG", "launch app"); ;

                    Animation ani = new Animation(150);
                    ani.AnimateTo(mainView, "Scale", new Vector3(0.9f, 0.9f, 1.0f));
                    ani.Play();
                    ani.Finished += Ani_Finished;
                }
                isMoving = false;
            }

            return true;
        }

        private void Ani_Finished(object sender, EventArgs e)
        {
            mainView.Scale = new Vector3(1.0f, 1.0f, 1.0f);
            launchApplication();
        }

        public static string GetResourcePath()
        {
            return Tizen.Applications.Application.Current.DirectoryInfo.Resource;
        }
        private void launchApplication()
        {
            AppControl appControl = new AppControl();
            appControl.ApplicationId = "org.tizen.example.NUIMusicPlayer";
            Window.Instance.SendLaunchRequest(appControl, true);
        }


        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
            if (e.Key.State == Key.StateType.Down && (e.Key.KeyPressedName == "XF86Back" || e.Key.KeyPressedName == "Escape"))
            {
                Exit();
            }
        }

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
