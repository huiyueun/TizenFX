using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Tizen.NUI.Samples
{
    public class GeometryCreator
    {
        private Color color;
        public GeometryCreator(Color color)
        {
            this.color = color;
        }

        public global::System.IntPtr MeshVertexDataPtr(List<Vector3> vertexList, List<Vector3> normalList, List<Vector2> uvs)
        {
            var vertices = new TexturedQuadVertex[vertexList.Count];
            var idx = 0;

            foreach (var vertex in vertexList)
            {
                vertices[idx].normal = new Vec3(normalList[idx].X, normalList[idx].Y, normalList[idx].Z);

                if(normalList[idx].X == 0 && normalList[idx].Y != 0 && normalList[idx].Z == 0)
                {
                    vertices[idx].vertColor = new Vec4(0.0f, 0.0f, 0.0f, 1.0f);
                }
                else
                {
                    vertices[idx].vertColor = new Vec4(color.R, color.G, color.B, color.A);
                }
                if (uvs != null)
                {
                    vertices[idx].texcoord = new Vec2(uvs[idx].X, uvs[idx].Y);
                }
                vertices[idx++].position = new Vec3(vertex.X / 300-1.5f, vertex.Z / 300-1.0f, vertex.Y / 300 - 0.0f);
            }

            int length = Marshal.SizeOf(vertices[0]);
            global::System.IntPtr pA = Marshal.AllocHGlobal(length * vertexList.Count);

            for (int i = 0; i < vertexList.Count; i++)
            {
                Marshal.StructureToPtr(vertices[i], pA + i * length, true);
            }

            return pA;
        }

        public ushort[] MeshIndexData(List<int> indexList)
        {
            var indices = new ushort[indexList.Count];
            var idx = 0;

            foreach (var index in indexList)
            {
                indices[idx++] = (ushort)index;
            }
            return indices;
        }

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

        public struct Vec3
        {
            float x;
            float y;
            float z;
            public Vec3(float xIn, float yIn, float zIn)
            {
                x = xIn;
                y = yIn;
                z = zIn;
            }
        }

        public struct Vec4
        {
            float r;
            float g;
            float b;
            float a;
            public Vec4(float xIn, float yIn, float zIn, float aIn)
            {
                r = xIn;
                g = yIn;
                b = zIn;
                a = aIn;
            }
        }

        struct TexturedQuadVertex
        {
            public Vec3 position;
            public Vec3 normal;
            public Vec2 texcoord;
            public Vec4 vertColor;
        }
    }
}
