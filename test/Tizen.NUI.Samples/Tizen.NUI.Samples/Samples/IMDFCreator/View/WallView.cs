using Space.BuildingTool.ProceduralMesh;
using System.Collections.Generic;
using Tizen.NUI.BaseComponents;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class WallView : IMDF3DView
    {
        private ModelRenderer meshRenderer;

        public WallView(RoomWall wallData)
        {
            Name = wallData.Id;

            Size = new Size(300, 300, 300);
            //Position = planeData.Coordinates.GetAverage();
            Position = new Position(0, 0, 0);

            CreateMesh(wallData);
        }

        private void CreateMesh(RoomWall wall)
        {
            List<Vector3> doors = new List<Vector3>();
            List<Vector3> windows = new List<Vector3>();
            foreach (var opening in wall.Openings)
            {
                if (opening.Category == OpeningCategory.DOOR)
                {
                    doors.AddRange(opening.Coordinates);
                }
                else if (opening.Category == OpeningCategory.WINDOW)
                {
                    windows.AddRange(opening.Coordinates);
                }
            }
            foreach (var vertex in wall.Coordinates)
            {
                Tizen.Log.Error("MYLOG", $"1 vertex :{vertex.X} {vertex.Y} {vertex.Z} \n");
            }
            //TODO : Window & Door is nUll
            var meshDraft =  MVMeshCreator.Instance.CreateWallMeshInfo(wall.Coordinates[0], wall.Coordinates[1], null, null);

            Tizen.Log.Error("MYLOG", $"vertex Count : {meshDraft.vertices.Count}\n");
            foreach (var vertex in meshDraft.vertices)
            {
                Tizen.Log.Error("MYLOG", $"vertex : {vertex.X}, {vertex.Y}, {vertex.Z}\n");
            }

            meshRenderer = new ModelRenderer();
            AddRenderer(meshRenderer.CreateMeshRenderer(meshDraft, "/images/PaletteTest/rock.jpg", new Color(0.7f, 0.7f, 0.7f, 1.0f)));
        }
    }
}
