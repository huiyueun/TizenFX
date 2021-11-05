using System;
using System.Collections.Generic;
using System.Text;
using Tizen.Applications;
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
                SizeHeight = 100,
                Position2D = new Position2D(0, 0),
                //BackgroundColor = Color.White
            };
            this.Add(titleView);

            label = new TextLabel()
            {
                HeightSpecification = LayoutParamPolicies.MatchParent,
                WidthSpecification = LayoutParamPolicies.MatchParent,
                PointSize = 48,
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
            titleView.Add(bottomLine);

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
                    Columns = 5,
                    GridOrientation = GridLayout.Orientation.Horizontal
                },
                WidthSpecification = LayoutParamPolicies.MatchParent,
                HeightSpecification = LayoutParamPolicies.WrapContent,
                Padding = new Extents(20, 20, 20, 10),
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

            String FolderName = Tizen.Applications.Application.Current.DirectoryInfo.Resource + "/icon/";
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

            int item_width = (Window.Instance.Size.Height - 10 * 2) / 5;
            int icon_width = 100;

            var item = new View
            {
                Layout = new AbsoluteLayout() { },
                Name = FileNameOnly,
                WidthSpecification = item_width,
                HeightSpecification = 150,
            };

            var appIcon = new View
            {
                Name = filename,
                BackgroundImage = fullname,
                Size2D = new Size2D(icon_width, icon_width),
                Position2D = new Position2D(item_width / 2 - (icon_width / 2), 30),
                LeaveRequired = true
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

                //Set TargetView
                ApplicationTransitionManager.Instance.SourceView = view;

                //Set Appearing Animation
                ApplicationTransitionManager.Instance.AppearingTransition = new FadeTransition()
                {
                    AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Default),
                    TimePeriod = new TimePeriod(400),
                };
                ApplicationTransitionManager.Instance.DisappearingTransition = new FadeTransition()
                {
                    AlphaFunction = new AlphaFunction(AlphaFunction.BuiltinFunctions.Default),
                    TimePeriod = new TimePeriod(400),
                };

                //Launch new application with transition
                var control = new AppControl()
                {
                    ApplicationId = "org.tizen.example.ProviderSample",
                };
                ApplicationTransitionManager.Instance.LaunchRequestWithTransition(control);
            }
            return false;
        }
    }
}
