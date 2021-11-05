using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUIAllAppsSample
{
    public class AppsView : View
    {
        private View titleView;
        private TextLabel label;

        private ScrollableBase scroll;
        public View gridView;

        public AppsView()
        {
            CreateMainListView();
            CreateGridView();
        }

        private void CreateMainListView()
        {
            //title view
            titleView = new View()
            {
                Layout = new AbsoluteLayout() { },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                SizeHeight = 60,
                Position2D = new Position2D(0, 0),
                BackgroundColor = new Color(0.4f, 0.4f, 0.4f, 0.2f),
            };
            this.Add(titleView);

            label = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 20,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Text = "All Apps",
                TextColor = Color.White,
            };
            titleView.Add(label);
            label.Name = label.Text;

            var bottomLine = new View
            {
                WidthSpecification = LayoutParamPolicies.MatchParent,
                SizeHeight = 100,
                Position2D = new Position2D(0, 10),
                BackgroundColor = new Color(0, 0, 0, 0.25f)
            };
            //titleView.Add(bottomLine);

            //scroll
            scroll = new ScrollableBase()
            {
                Layout = new LinearLayout()
                {
                    LinearOrientation = LinearLayout.Orientation.Vertical,
                    LinearAlignment = LinearLayout.Alignment.Top,
                },
                Position2D = new Position2D(0, 100),
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,
                ScrollingDirection = ScrollableBase.Direction.Vertical,
            };
            scroll.EnableOverShootingEffect = true;
            this.Add(scroll);
        }

        private void CreateGridView()
        {
            gridView = new View
            {
                Layout = new GridLayout()
                {
                    Columns = 4,
                    GridOrientation = GridLayout.Orientation.Horizontal
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents(10, 20, 0, 10),
                //BackgroundColor = Color.White
            };
            scroll.Add(gridView);

            LoadAppList();
        }


        public void LoadAppList()
        {
            uint size = gridView.ChildCount;
            for (uint i = 1; i <= size; i++)
            {
                View child = gridView.GetChildAt(size - i);
                if (child != null)
                {
                    gridView.Remove(child);
                    child.Dispose();
                }
            }

            String FolderName = "./res/icon/";
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(FolderName);
            foreach (System.IO.FileInfo File in di.GetFiles())
            {
                if (File.Extension.ToLower().CompareTo(".png") == 0)
                {
                    string name = File.Name;
                    String fullName = File.FullName;
                    gridView.Add(CreateInstalledItem(name, fullName));
                }
            }
        }

        private View CreateInstalledItem(string filename, string fullname)
        {
            String FileNameOnly = fullname.Substring(0, fullname.Length - 4);
            FileNameOnly = string.Format("{0}{1}", char.ToUpper(FileNameOnly[0]), FileNameOnly.Remove(0, 1));

            int item_width = (Window.Instance.Size.Width - 10 * 2) / 4;
            int icon_width = 70;

            var item = new View
            {
                Layout = new AbsoluteLayout() { },
                Name = FileNameOnly,
                WidthSpecification = (int)(item_width*1.05f),
                HeightSpecification = 100,
            };

            var appIcon = new View
            {
                Name = filename,
                BackgroundImage = fullname,
                Size2D = new Size2D(icon_width, icon_width),
                Position2D = new Position2D(item_width / 2 - (icon_width / 2), 30),
                LeaveRequired = true,

                CornerRadius = 0.5f,
                CornerRadiusPolicy = VisualTransformPolicyType.Relative,
                TransitionOptions = new TransitionOptions()
                {
                    TransitionTag = filename,
                },
            };
            appIcon.TouchEvent += AppIcon_TouchEvent;
            item.Add(appIcon);
            return item;
        }

        private bool AppIcon_TouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                var view = source as View;

                LaunchApp(view.Name);
                //Launch new application with transition
                /*
                var control = new AppControl()
                {
                    ApplicationId = "org.tizen.example.ProviderSample",
                };
                ApplicationTransitionManager.Instance.LaunchRequestWithTransition(control);*/
            }
            return false;
        }


        private void LaunchApp(string name)
        {
            var mainView = new View()
            {
                Layout = new AbsoluteLayout() { },
                BackgroundColor = Tizen.NUI.Color.Red,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.MatchParent,

                TransitionOptions = new TransitionOptions()
                {
                    TransitionTag = name,
                },
            };
            mainView.TouchEvent += main_TouchEvent;
            var appView = new DialogPopup()
            {
                EnableDismissOnScrim = true,
                BackgroundColor = Tizen.NUI.Color.Transparent,
                Content = mainView,
            };
            Window.Instance.GetDefaultNavigator().PushWithTransition(appView);
        }

        private bool main_TouchEvent(object source, View.TouchEventArgs e)
        {
            if (e.Touch.GetState(0) == PointStateType.Up)
            {
                var view = source as View;
                Window.Instance.GetDefaultNavigator().PopWithTransition();
            }
            return false;
        }
    }
}
