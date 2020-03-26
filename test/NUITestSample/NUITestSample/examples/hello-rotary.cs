
using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using Tizen.NUI.Components;

namespace NUITestSample
{
    public class HelloRotary : NUIApplication
    {
        RotarySelector2 rotarySelector;
        TextLabel modelabel;
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        public void Initialize()
        {
          Window window = Window.Instance;
          window.BackgroundColor = Color.White;
          Window.Instance.KeyEvent += OnKeyEvent;

          InitializeDefaultUI();
        }

        public void InitializeDefaultUI()
        {
          TextLabel label = new TextLabel()
          {
            Text = "RotarySelector Sample",
            PointSize = 20,
          };
          Window.Instance.Add(label);

          rotarySelector = new RotarySelector2()
          {
            Size2D =  new Size2D(480,600),
            Position2D = new Position2D(0,30),
            BackgroundColor = Color.Black,
          };

          for(int i = 1; i <= 50; i++)
          {
            RotarySelectorItem item = new RotarySelectorItem()
            {
              Size = new Size(50,50),
              BackgroundColor = Color.White,
              MainText = "Main " + i,
              SubText = "Sub " + i,
            };

            TextLabel numLabel = new TextLabel()
            {
              Text = "" +(i),
              PointSize = 10,
              TextColor = Color.Black,
            };
            item.Add(numLabel);

            rotarySelector.AppendItem(item);
          }
          
          Window.Instance.Add(rotarySelector);

          modelabel = new TextLabel()
          {
            Text = "Mode : NormalMode",
            PointSize = 16,
            TextColor = Color.White,
            Position2D = new Position2D(0,30),
          };
          Window.Instance.Add(modelabel);

          View bottomView = new View()
          {
              Size = new Size(480, 100),
              Position2D = new Position2D(0,600),
              BackgroundColor = Color.Cyan,
              Layout = new LinearLayout()
              {
                LinearOrientation = LinearLayout.Orientation.Horizontal,
                LinearAlignment = LinearLayout.Alignment.Center,
              }
          };
          Window.Instance.Add(bottomView);

          Button btn_1 = new Button()
          {
            Text = "ChangeMode",
            Size = new Size(100,50),
            Margin = 10,
          };
          btn_1.ClickEvent += Btn1_ClickEvent;
          bottomView.Add(btn_1);

          View bottomView_2 = new View()
          {
              Size = new Size(480, 100),
              Position2D = new Position2D(0,700),
              BackgroundColor = Color.Yellow,
              Layout = new LinearLayout()
              {
                LinearOrientation = LinearLayout.Orientation.Horizontal,
                LinearAlignment = LinearLayout.Alignment.Center,
              }
          };
          Window.Instance.Add(bottomView_2);

          Button btn_5 = new Button()
          {
            Text = "<<",
            Size = new Size(50,50),
            Margin = 10,
          };
          Button btn_6 = new Button()
          {
            Text = ">>",
            Size = new Size(50,50),
            Margin = 10,
          };
          btn_5.ClickEvent += Btn5_ClickEvent;
          btn_6.ClickEvent += Btn6_ClickEvent;

          bottomView_2.Add(btn_5);
          bottomView_2.Add(btn_6);
        }
        
        private void Btn1_ClickEvent(object sender, Button.ClickEventArgs e)
        {
          rotarySelector.IsEditMode = !rotarySelector.IsEditMode;
          modelabel.Text = (rotarySelector.IsEditMode ? "Mode : EditMode" : "Mode : NormalMode" );
        }

        private void Btn5_ClickEvent(object sender, Button.ClickEventArgs e)
        {
          rotarySelector.PrevPage();
        }
        
        private void Btn6_ClickEvent(object sender, Button.ClickEventArgs e)
        {
          rotarySelector.NextPage();
        }

        public void OnKeyEvent(object sender, Window.KeyEventArgs e)
        {
          if (e.Key.State == Key.StateType.Down )
          {
            switch(e.Key.KeyPressedName)
            {
              case "XF86Back":
              case "Escape":
              {
                Exit();
                break;
              }
            }
          }
        }

        [STAThread]
        static void _Main(string[] args)
        {
            HelloRotary example = new HelloRotary();
            example.Run(args);
        }
    }
}