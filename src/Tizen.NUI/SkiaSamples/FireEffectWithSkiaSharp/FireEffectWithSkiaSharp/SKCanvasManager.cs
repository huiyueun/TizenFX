using OpenTK.Graphics.ES20;
using OpenTK.Platform;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace CubeWithSkiaSharp
{
    public class SKCanvasManager
    {
        public List<Circle> Circles = new List<Circle>();

        private SKPaint paint = new SKPaint();
        private SKPaint filterPaint = new SKPaint();

        // Memory block which SKSurface will be create on it.
        private IntPtr pBitMap;
        // SKSurface used to draw text, it created on the memory block
        private SKSurface surface;
        // SKCanvas used to draw text
        private SKCanvas canvas;

        // row bytes of bitmap
        private int rowByte;
        // size of bitmap
        private int bitmapHeight, bitmapWidth;

        /// <summary>
        /// Create SKCanvas on the memory block
        /// </summary>
        public void CreateSKCanvas()
        {
            paint.IsAntialias = true;
            paint.StrokeWidth = 1;
            paint.IsStroke = true;

            var imageFilter = SKImageFilter.CreateBlur(10, 10);
            var colorFilter = SKColorFilter.CreateColorMatrix(
                new float[]
                {
                         1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                         0.0f, 1.0f, 0.0f, 0.0f, 0.0f,
                         0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                         0.0f, 0.0f, -8.0f, 21.0f, 0.0f
                });
            filterPaint = new SKPaint();
            filterPaint.IsAntialias = true;
            filterPaint.ImageFilter = SKImageFilter.CreateColorFilter(colorFilter, imageFilter);

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 4);

            // create the SKSurface
            var info = new SKImageInfo(bitmapWidth, bitmapHeight, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            surface = SKSurface.Create(info, pBitMap, rowByte);
            if (surface != null)
            {
                canvas = surface.Canvas;
            }
        }

        public void DrawSKCanvas()
        {
            canvas.DrawColor(SKColors.Transparent);

            if (new Random().NextDouble() > .25)
            {
                Circles.Add(new Circle(bitmapWidth / 2, bitmapHeight / 2, 1 + new Random().NextDouble()));
                for (var i = 0; i < Circles.Count; i++)
                {
                    Circles[i].Update(bitmapWidth, bitmapHeight, Circles);
                    Circles[i].Render(paint, canvas);
                }

                using (var image = surface.Snapshot())
                {
                    canvas.DrawColor(SKColors.Black);
                    canvas.DrawImage(image, new SKPoint(0, 0), filterPaint);
                    canvas.Flush();
                }
                Create2DTextureFromMemory();

                Tizen.Log.Error("MYLOG", "Cirlces :" + Circles.Count);
            }
        }

        public void CreateBitmapInfo(IGameWindow mainWindow)
        {
            // set bitmap size as half of the window size
            bitmapHeight = 200;// (int)(mainWindow.Height);
            bitmapWidth = 200;// (int)(mainWindow.Width);

            pBitMap = Marshal.AllocHGlobal(bitmapWidth * bitmapHeight * 4);

            rowByte = bitmapWidth * 4;
        }

        public void FreeBitmap()
        {
            if (pBitMap != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(pBitMap);
                pBitMap = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Create 2D texture from memory block
        /// </summary>
        private void Create2DTextureFromMemory()
        {
            GL.TexImage2D(TextureTarget2d.Texture2D, 0, TextureComponentCount.Rgba, bitmapWidth, bitmapHeight, 0, PixelFormat.Rgba, PixelType.UnsignedByte, pBitMap);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)All.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)All.Linear);
            GL.GenerateMipmap(TextureTarget.Texture2D);
        }
    }
}
