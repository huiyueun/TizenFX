/*
* Copyright (c) 2017 Samsung Electronics Co., Ltd.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*
*/

using System;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;

namespace HelloWorldTest
{
    class Example : NUIApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            Initialize();
        }

        public void Initialize()
        {
            Window window = Window.Instance;
            window.BackgroundColor = Color.White;

            View view = new View()
            {
              WidthResizePolicy = ResizePolicyType.FitToChildren,
              HeightResizePolicy = ResizePolicyType.FitToChildren,
              BackgroundColor = Color.Red,
              Layout = new GridLayout()
              {
                Columns = 3
              },
            };

           for(int i = 0; i<30;i++)
           {
             View child = new View()
             {
               Size = new Size(100,100),
               BackgroundColor = i%2 == 0?Color.Magenta:Color.Cyan,
               Margin = 10
             };
             view.Add(child);
           }
          Window.Instance.Add(view);

        }

        [STAThread]
        static void _Main(string[] args)
        {
            Example example = new Example();
            example.Run(args);
        }
    }
}
