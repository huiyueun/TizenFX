using System;
using System.Runtime.InteropServices;
using Tizen.NUI;
using Tizen.NUI.BaseComponents;
using SkiaSharp;

namespace SampleNUIApp
{
    class ShaderEffectView : View
    {
        private SkiaCanvasManager skiaCanvasManager;
        private Shader shader;

        [StructLayout(LayoutKind.Sequential)]
        public struct Vec2
        {
            float x;
            float y;
            public Vec2(float xIn, float yIn)
            {
                x = xIn;
                y = yIn;
            }
        }

        public struct TexturedQuadVertex
        {
            public Vec2 position;
            // public Vec2 textureCoordinates;
        };

        public static byte[] Struct2Bytes(TexturedQuadVertex[] obj)
        {
            int size = Marshal.SizeOf(obj);
            byte[] bytes = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, false);
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }

        static readonly string VERTEX_SHADER =
          "attribute mediump vec2 aPosition;\n" +
          "uniform mediump mat4 uMvpMatrix;\n" +
          "uniform mediump vec3 uSize;\n" +
          "varying mediump vec2 vTexCoord;\n" +
          "void main()\n" +
          "{\n" +
          "    gl_Position = uMvpMatrix * vec4(aPosition * uSize.xy, 0.0, 1.0);\n" +
            "  vTexCoord = aPosition + vec2( 0.5 );\n" +
          "}\n";


        static readonly string FRAGMENT_SHADER =
        "precision mediump float;\n" +
        "varying mediump vec2 vTexCoord;\n" +
        "uniform lowp vec4 uColor;\n" +
        "uniform sampler2D sTexture;\n" +
        "void main()\n" +
        "{\n" +
            "vec2 uv = vec2(vTexCoord.xy / vec2(720,1280));"+
            "lowp vec4 color = texture2D( sTexture, vTexCoord ) * uColor;\n" +
            "gl_FragColor = color;\n" +
        "}\n";


        private Renderer renderer;

        public ShaderEffectView(Size2D size, Position2D pos)
        {
            skiaCanvasManager = new SkiaCanvasManager();

            /* Create Shader */
            shader = new Shader(VERTEX_SHADER, FRAGMENT_SHADER);

            /* Create Property buffer */
            var vertexFormat = new PropertyMap();
            vertexFormat.Add("aPosition", new PropertyValue((int)PropertyType.Vector2));

            var vertexBuffer = new PropertyBuffer(vertexFormat);
            vertexBuffer.SetData(RectangleDataPtr(), 4);

            var geometry = new Geometry();
            geometry.AddVertexBuffer(vertexBuffer);
            geometry.SetType(Geometry.Type.TRIANGLE_STRIP);

            this.Size2D = new Size(250,250);
            this.ParentOrigin = Tizen.NUI.ParentOrigin.Center;
            this.PivotPoint = Tizen.NUI.PivotPoint.Center;
            this.PositionUsesPivotPoint = true;
            this.BackgroundColor = Color.Black;
            this.Position2D = pos;
            this.Orientation = new Rotation(new Radian(new Degree(0.0f)), PositionAxis.Z);

            renderer = new Renderer(geometry, shader);
            renderer.BlendMode = 0;

            skiaCanvasManager.CreateBitmapInfo(size.Width, size.Height);
            skiaCanvasManager.CreateSKCanvas();
            skiaCanvasManager.DrawSKCanvas(renderer);
        }

        public void SetRenderer(TextureSet textureSet = null)
        {
            if (textureSet != null)
            {
                renderer.SetTextures(textureSet);
            }
            else
            {
                renderer.SetTextures(skiaCanvasManager.CreateTextureSet());
            }
            this.AddRenderer(renderer);
        }

        public void StartDrawing()
        {
            var timer = new Timer(50);
            timer.Tick += OnTimerTick;
            timer.Start();
        }
        private bool OnTimerTick(object source, Timer.TickEventArgs e)
        {
            skiaCanvasManager.DrawSKCanvas(renderer);
            skiaCanvasManager.UpdateTexture();

            return true;
        }

        private IntPtr RectangleDataPtr()
        {
            var vertex1 = new TexturedQuadVertex();
            var vertex2 = new TexturedQuadVertex();
            var vertex3 = new TexturedQuadVertex();
            var vertex4 = new TexturedQuadVertex();
            vertex1.position = new Vec2(-0.5f, -0.5f);
            vertex2.position = new Vec2(-0.5f, 0.5f);
            vertex3.position = new Vec2(0.5f, -0.5f);
            vertex4.position = new Vec2(0.5f, 0.5f);

            var texturedQuadVertexData = new TexturedQuadVertex[4] { vertex1, vertex2, vertex3, vertex4 };

            var lenght = Marshal.SizeOf(vertex1);
            var pA = Marshal.AllocHGlobal(lenght * 4);

            for (int i = 0; i < 4; i++)
            {
                Marshal.StructureToPtr(texturedQuadVertexData[i], pA + i * lenght, true);
            }

            return pA;
        }
    }
}
