using System;
using System.Collections.Generic;
using System.Text;
using Tizen.NUI.Accessibility;
using Tizen.NUI.BaseComponents;

namespace NUIAllAppsSample
{
    public static class AccessibilityMgr
    {
        private static uint count = 1;
        private static AccessibilityManager manager = AccessibilityManager.Instance;
        private static List<View> viewList = new List<View>();
        private static Dictionary<string, string> viewDic = new Dictionary<string, string>();

        public static void init()
        {
            manager.ActionActivate += OnActionActivate;
        }

        private static bool OnActionActivate(object source, EventArgs e)
        {

            View cview = manager.GetCurrentFocusView();

            if (cview != null)
            {
                viewDic.TryGetValue(cview.Name, out string appid);
                if (!string.IsNullOrEmpty(appid))
                {
                   // Util.GetInstance().LaunchApp(appid);
                    manager.ClearFocus();
                }
            }

            return true;
        }

        public static void AddView(View view)
        {
            manager.SetFocusOrder(view, count++);
            manager.SetAccessibilityAttribute(view, AccessibilityManager.AccessibilityAttribute.Value, view.Name);
            viewList.Add(view);
        }

        public static void AddView(View view, string appid)
        {
            manager.SetFocusOrder(view, count++);
            manager.SetAccessibilityAttribute(view, AccessibilityManager.AccessibilityAttribute.Value, view.Name);
            viewDic.Add(view.Name, appid);
            viewList.Add(view);
        }

        public static void UpdateView(View view)
        {
            manager.SetAccessibilityAttribute(view, AccessibilityManager.AccessibilityAttribute.Value, view.Name);
        }

        public static void setFocus(View view)
        {
            manager.SetCurrentFocusView(view);
        }

        public static void Reset()
        {
            count = 1;
            viewList.Clear();
            manager.Reset();
        }

        public static void OnPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Name")
            {
                if (viewList.Contains(sender as View))
                {
                    UpdateView(sender as View);
                }
                else
                {
                    AddView(sender as View);
                }
            }
        }
    }

}
