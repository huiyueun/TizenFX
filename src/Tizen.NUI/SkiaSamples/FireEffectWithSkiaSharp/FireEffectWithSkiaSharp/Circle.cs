using SkiaSharp;
using System;
using System.Collections.Generic;

namespace CubeWithSkiaSharp
{
    public class Circle
    {
        private string[] colorPallete = { "#ff2121", "#ff7221", "#ffe420" };

        private double x;
        private double y;
        private double angle;
        private double vx;
        private double vy;
        private double r;
        private SKColor color;

        public Circle(float x, float y, double speed)
        {
            this.x = x;
            this.y = y;
            this.angle = RandomInRange(Math.PI + Math.PI / 2.7f, Math.PI * 2 - Math.PI / 2.75f);
            this.vx = speed * Math.Cos(angle);
            this.vy = speed * Math.Sin(angle);
            this.r = RandomInRange(6, 12);

            color = GetRandomColor();
        }

        public void Update(double w, double h, List<Circle> circles)
        {
            this.x += (this.vx *= 1.005f);
            this.y += (this.vy *= 1.005f);
            this.r -= .031;
            if (this.x + this.r < 0 ||
                this.x - this.r > w/2 ||
                this.x - this.r > h/2 ||
                this.r <= 0.5f)
            {
                var circle = circles.IndexOf(this);
                circles.RemoveAt(circle);
                GC.SuppressFinalize(circle);
            }
        }

        public void Render(SKPaint paint, SKCanvas canvas)
        {
            paint.Color = color;
            canvas.DrawCircle((float)this.x, (float)this.y, (float)this.r, paint);
        }

        private SKColor GetRandomColor()
        {
            var hexColor = colorPallete[new Random().Next(0, 3)];
            SKColor.TryParse(hexColor, out color);
            return new SKColor(color.Blue, color.Green, color.Red, color.Alpha);
        }

        private float RandomInRange(double min, double max)
        {
            return (float)(new Random().NextDouble() * (max - min) + min);
        }
    }
}
