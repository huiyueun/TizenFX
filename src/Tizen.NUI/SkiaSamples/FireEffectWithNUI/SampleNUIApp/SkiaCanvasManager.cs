using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Tizen.NUI;

namespace SampleNUIApp
{
    public class SkiaCanvasManager
    {
        public List<Circle> Circles = new List<Circle>();

        private SKPaint paint = new SKPaint();
        private SKPaint filterPaint = new SKPaint();

        private IntPtr pBitMap;
        private SKSurface surface;
        private SKCanvas canvas;

        private int rowByte;
        private int bitmapHeight, bitmapWidth;

        private TextureSet textureSet;
        private Texture uploadTexture;

        public void CreateSKCanvas()
        {
            paint.IsAntialias = true;
            paint.StrokeWidth = 3;
            paint.IsStroke = true;

            var imageFilter = SKImageFilter.CreateBlur(10, 10);
            var colorFilter = SKColorFilter.CreateColorMatrix(
                new float[]
                {
                         1.0f, 0.0f, 0.0f, 0.0f, 0.0f,
                         0.0f, 1.0f, 0.0f, 0.0f, 0.0f,
                         0.0f, 0.0f, 1.0f, 0.0f, 0.0f,
                         0.0f, 0.0f, -13.0f, 21.0f, 0.0f
                });
            filterPaint.IsAntialias = true;
            filterPaint.ImageFilter = SKImageFilter.CreateColorFilter(colorFilter, imageFilter);


            // create the SKSurface
            var info = new SKImageInfo(bitmapWidth, bitmapHeight, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
            surface = SKSurface.Create(info, pBitMap, rowByte);
            if (surface != null)
            {
                canvas = surface.Canvas;
            }
        }

        public void DrawSKCanvas(Renderer renderer)
        {
            if (new Random().NextDouble() > .25)
            {
                canvas.DrawColor(SKColors.Transparent);
                Circles.Add(new Circle(bitmapWidth / 2, bitmapHeight / 2, 1 + new Random().NextDouble()));
                for (var i = 0; i < Circles.Count; i++)
                {
                    Circles[i].Update(null, bitmapWidth, bitmapHeight, Circles);
                    Circles[i].Render(paint, canvas);
                }
                var image = surface.Snapshot();
                canvas.DrawColor(SKColors.Black);
                //canvas.DrawImage(image, new SKPoint(0, 0));
                canvas.DrawImage(image, new SKPoint(0, 0), filterPaint);
                canvas.Flush();
            }
        }

        public void CreateBitmapInfo(int width, int height)
        {
            bitmapHeight = (int)(0.5f * height);
            bitmapWidth = (int)(0.5f * width);
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

        public TextureSet CreateTextureSet()
        {
            if (uploadTexture == null)
            {
                uploadTexture = new Texture(TextureType.TEXTURE_2D, PixelFormat.RGBA8888, (uint)bitmapWidth, (uint)bitmapHeight);
            }

            if (textureSet == null)
            {
                textureSet = new TextureSet();
                textureSet.SetTexture(0, uploadTexture);
            }
            //UpdateTexture();
            return textureSet;
        }

        public void UpdateTexture()
        {
            var snapShot = surface.Snapshot();
            var pixelBuffer = ImageLoading.LoadImageFromBuffer(snapShot.Encode().AsStream());
            var pixelData = PixelBuffer.Convert(pixelBuffer);
            uploadTexture.Upload(pixelData);
        }
    }
}
