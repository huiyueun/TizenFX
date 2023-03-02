using Space.BuildingTool.ProceduralMesh;

namespace Tizen.NUI.Samples
{
    public class ModelRenderer
    {
        static readonly string VERTEX_SHADER =
            "attribute mediump vec3 aPosition;\n" +
            "attribute mediump vec3 aNormal;\n" +
            "attribute mediump vec2 aTexCoord;\n" +
            "attribute mediump vec4 aColor;\n" +
            "uniform mediump mat4 uMvpMatrix;\n" +
            "uniform mediump mat3 uNormalMatrix;\n" +
            "uniform mediump vec3 uSize;\n" +
            "varying mediump vec3 vNormal;\n" +
            "varying mediump vec2 vTexCoord;\n" +
            "varying mediump vec3 vPosition;\n" +
            "varying mediump vec4 vColor;\n" +
            "void main()\n" +
            "{\n" +
            "    vec4 pos = vec4(aPosition, 1.0)*vec4(uSize,1.0);\n" +
            "    gl_Position = uMvpMatrix*pos;\n" +
            "    vPosition = aPosition;\n" +
            "    vNormal   = normalize(uNormalMatrix * aNormal);\n" +
            "    vTexCoord = aTexCoord;\n" +
            "    vColor = aColor;\n" +
            "}\n";

        static readonly string FRAGMENT_SHADER =
            "uniform lowp vec4 uColor;\n" +
            "uniform sampler2D sTexture;\n" +
            "varying mediump vec3 vNormal;\n" +
            "varying mediump vec2 vTexCoord;\n" +
            "varying mediump vec3 vPosition;\n" +
            "varying mediump vec4 vColor;\n" +
            "mediump vec3 uLightDir = vec3(2.0, 0.5, 1.0);\n" + // constant light dir
            "mediump vec3 uViewDir  = vec3(0.0, 0.0, 1.0);\n" + // constant view dir.
            "mediump vec3 uAmbientColor = vec3(0.60, 0.60, 0.60);\n" +
            "mediump vec3 uDiffuseColor = vec3(0.8, 0.8, 0.8);\n" +
            "mediump vec3 uSpecularColor = vec3(0.5, 0.5, 0.5);\n" +
            "void main()\n" +
            "{\n" +
            "    mediump vec3 lightdir = normalize(uLightDir);\n" +
            "    mediump vec3 eyedir   = normalize(uViewDir);\n" +
            "    mediump vec4 texColor = uColor * vColor;\n" +//texture2D( sTexture, vTexCoord ) * uColor * vColor;\n" +
            "    mediump float diffuse = min(max(-dot(vNormal, lightdir) + 0.1, 0.0), 1.0);\n" +
            "    mediump vec3 reflectdir = reflect(-lightdir, vNormal);\n" +
            "    mediump float specular = pow(max(0.0, dot(reflectdir, eyedir)), 50.0);\n" +
            "    mediump vec4 color = texColor * vec4(uAmbientColor + uDiffuseColor * diffuse, 1.0) + vec4(uSpecularColor, 0.0) * specular;\n" +
            "    gl_FragColor = color;\n" +
            "}\n";

        public Renderer CreateMeshRenderer(MeshDraft md, string texture, Color color)
        {
            var renderer = new Renderer(GenerateGeometry(md, color), new Shader(VERTEX_SHADER, FRAGMENT_SHADER));
//            renderer.SetTextures(CreateTexture(texture));

            return renderer;
        }

        private TextureSet CreateTexture(string resource)
        {
            var pixelData = PixelBuffer.Convert(ImageLoader.LoadImageFromFile(
                Tizen.Applications.Application.Current.DirectoryInfo.Resource + resource,
                new Size2D(),
                FittingModeType.ScaleToFill
            ));

            var texture = new Texture(
                TextureType.TEXTURE_2D,
                pixelData.GetPixelFormat(),
                pixelData.GetWidth(),
                pixelData.GetHeight()
            );
            texture.Upload(pixelData);

            var textureSet = new TextureSet();
            textureSet.SetTexture(0u, texture);
            return textureSet;
        }

        private Geometry GenerateGeometry(MeshDraft md, Color color)
        {
            var geometryCreator = new GeometryCreator(color);

            var vertexFormat = new PropertyMap();
            vertexFormat.Add("aPosition", new PropertyValue((int)PropertyType.Vector3));
            vertexFormat.Add("aNormal", new PropertyValue((int)PropertyType.Vector3));
            vertexFormat.Add("aTexCoord", new PropertyValue((int)PropertyType.Vector2));
            vertexFormat.Add("aColor", new PropertyValue((int)PropertyType.Vector4));

            var vertexBuffer = new PropertyBuffer(vertexFormat);
            vertexBuffer.SetData(geometryCreator.MeshVertexDataPtr(md.vertices, md.normals), (uint)md.vertices.Count);

            var indexBuffer = geometryCreator.MeshIndexData(md.triangles);

            var geometry = new Geometry();
            geometry.AddVertexBuffer(vertexBuffer);
            geometry.SetIndexBuffer(indexBuffer, (uint)md.triangles.Count);
            geometry.SetType(Geometry.Type.TRIANGLES);
            return geometry;
        }
    }
}
