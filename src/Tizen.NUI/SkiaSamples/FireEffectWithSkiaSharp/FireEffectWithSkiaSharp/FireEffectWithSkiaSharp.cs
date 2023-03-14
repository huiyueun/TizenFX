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
using CubeWithSkiaSharp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES20;
using OpenTK.Platform;
using OpenTK.Platform.Tizen;

namespace OpenTKSample
{
    public class FireEffectWithSkiaSharp : TizenGameApplication
    {
        private SKCanvasManager skCanvasManager = new SKCanvasManager();

        private int mProgramHandle;
        private int positionLoc, texCoordLoc, textureLoc, mvpLoc;
        private IGameWindow mainWindow;

        private readonly float[] vertices;
        private readonly float[] textCoord;

        private Matrix4 mvpMatrix;
        private Matrix4 viewMatrix;
        private Matrix4 modelMatrix;
        private int _texture;

        private readonly string vertexShaderSrc =
                                "uniform mat4 u_mvpMatrix;                  \n" +
                                "attribute vec4 a_position;                 \n" +
                                "attribute vec2 a_texCoord;                 \n" +
                                "varying vec2 v_texCoord;                   \n" +
                                "void main()                                \n" +
                                "{                                          \n" +
                                "   gl_Position = u_mvpMatrix * a_position; \n" +
                                "   v_texCoord = a_texCoord;                \n" +
                                "}                                          \n";
        private readonly string fragmentShaderSrc =
                                "precision mediump float;                   \n" +
                                "varying vec2 v_texCoord;                   \n" +
                                "uniform sampler2D s_texture;               \n" +
                                "void main()                                \n" +
                                "{                                          \n" +
                                "  gl_FragColor = texture2D( s_texture, v_texCoord );\n" +
                                "}                                          \n";

        public FireEffectWithSkiaSharp()
        {
            vertices = new float[]
            {
                /* front surface is blue */
                0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f, 0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f, -0.5f, 0.5f, 0.0f, 0.0f, 1.0f,
            };

            textCoord = new float[]
            {
                /* Texture coordinate of front surface*/
                1.0f, 0.0f,   0.0f, 1.0f,   1.0f, 1.0f,   1.0f, 0.0f,   0.0f, 0.0f,   0.0f, 1.0f,
            };
        }

        protected override void OnCreate()
        {
            base.OnCreate();
            mainWindow = Window;
            mainWindow.RenderFrame += OnRenderFrame;

            LoadApp();
        }

        protected override void OnTerminate()
        {
            base.OnTerminate();
            GL.DeleteTextures(1, ref _texture);
            skCanvasManager.FreeBitmap();
        }

        private void InitShader()
        {
            mProgramHandle = ShaderHelper.BuildProgram(vertexShaderSrc, fragmentShaderSrc);
            GL.BindAttribLocation(mProgramHandle, 0, "a_position");
            GL.BindAttribLocation(mProgramHandle, 1, "a_texCoord");
            GL.LinkProgram(mProgramHandle);

            GL.UseProgram(mProgramHandle);
        }

        private void LoadApp()
        {
            InitShader();

            GL.ClearColor(Color4.DarkSlateGray);
            GL.Enable(EnableCap.DepthTest);

            CreateBitmap();
            skCanvasManager.CreateSKCanvas();

            SetCubePosition();
        }


        private void OnUnload(Object sender, EventArgs e)
        {
            GL.DeleteTextures(1, ref _texture);
            skCanvasManager.FreeBitmap();
        }

        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="sender">the subject of RenderFrame Event </param>
        /// <param name="e">Contains timing information.</param>
        private void OnRenderFrame(Object sender, FrameEventArgs e)
        {
            GL.Viewport(0, 0, mainWindow.Width, mainWindow.Height);

            skCanvasManager.DrawSKCanvas();

            GL.ClearColor(Color4.Black);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.UseProgram(mProgramHandle);
            positionLoc = GL.GetAttribLocation(mProgramHandle, "a_position");
            texCoordLoc = GL.GetAttribLocation(mProgramHandle, "a_texCoord");
            textureLoc = GL.GetUniformLocation(mProgramHandle, "s_texture");
            GL.Uniform1(textureLoc, 0);

            unsafe
            {
                fixed (float* pvertices = vertices)
                {
                    // Prepare the vertex coordinate data
                    GL.VertexAttribPointer(positionLoc, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), new IntPtr(pvertices));
                    GL.EnableVertexAttribArray(positionLoc);
                }

                fixed (float* texCoord = textCoord)
                {
                    // Prepare the texture coordinate data
                    GL.VertexAttribPointer(texCoordLoc, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), new IntPtr(texCoord));
                    GL.EnableVertexAttribArray(texCoordLoc);
                }

            }

            mvpLoc = GL.GetUniformLocation(mProgramHandle, "u_mvpMatrix");

            // Apply the projection and view transformation
            GL.UniformMatrix4(mvpLoc, false, ref mvpMatrix);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 6);
            GL.Finish();

            // Disable vertex array
            GL.DisableVertexAttribArray(positionLoc);
            mainWindow.SwapBuffers();
        }

        /// <summary>
        /// Allocate a memory block with the size of bitmap.
        /// </summary>
        private void CreateBitmap()
        {
            skCanvasManager.FreeBitmap();
            skCanvasManager.CreateBitmapInfo(mainWindow);
        }

        private void SetCubePosition()
        {
            MatrixHelper.EsMatrixLoadIdentity(ref viewMatrix);
            viewMatrix = MatrixHelper.EsPerspective(60.0f, (float)mainWindow.Width / (float)mainWindow.Height, 1.0f, 20.0f, viewMatrix);

            MatrixHelper.EsMatrixLoadIdentity(ref modelMatrix);
            MatrixHelper.EsTranslate(ref modelMatrix, 0.0f, 0.0f, -2.5f);
            MatrixHelper.EsRotate(ref modelMatrix, 0, 0.0f, 1.0f, 0.0f);
            mvpMatrix = Matrix4.Mult(modelMatrix, viewMatrix);
        }

        static void Main(string[] args)
        {
            using (var game = new FireEffectWithSkiaSharp() { GLMajor = 2, GLMinor = 0 })
            {
                game.Run(args);
            }
        }
    }
}
