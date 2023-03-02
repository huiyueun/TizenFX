using System;
using System.Collections.Generic;
using ViewModel;
using Space.BuildingTool.Utility;
using Tizen.NUI;

namespace Space.BuildingTool.ProceduralMesh
{
    public class MVMeshCreator
    {
        public class MeshCreateOption
        {
            public float thicknessFactor = 5.0f;
            public float thicknessPercentage = 1.0f;
            public float openingThicknessPercentage = 0.99f;
            public float doorHeightPercentage = 1.0f;
            public float windowYScalePercentage = 0.5f;
            public bool stretchWallFollowThickness = true;
        }

        public MeshCreateOption defaultCreateOption { get; set; } = new MeshCreateOption();
        private static readonly Lazy<MVMeshCreator> _instance = new Lazy<MVMeshCreator>(() => new MVMeshCreator());
        public static MVMeshCreator Instance { get { return _instance.Value; } }

        private MeshCreator _meshCreator;

        private MVMeshCreator()
        {
            _meshCreator = new MeshCreator(new LibTessTessellator());
        }

        public MeshDraft CreatePlaneMeshInfo(List<Vector3> vertices)
        {
            return _meshCreator.CreatePlanarMesh(vertices);
        }

        public MeshDraft CreateOpeningMeshInfo(
            Vector3 topLeft, Vector3 topRight,
            OpeningCategory category, MeshCreateOption option = null)
        {
            option = option == null ? defaultCreateOption : option;
            List<Vector3> vertices = new List<Vector3>();

            if (OpeningCategory.DOOR == category)
            {
                vertices = PMLib.ApplyHeight(topLeft, topRight, topLeft.Y * option.doorHeightPercentage);
            }
            else if (OpeningCategory.WINDOW == category)
            {
                vertices = PMLib.ApplyHeight(topLeft, topRight, topLeft.Y * (option.windowYScalePercentage * 1.5f), topLeft.Y * (option.windowYScalePercentage * 0.5f));
            }

            float thickness = option.thicknessFactor * option.thicknessPercentage * option.openingThicknessPercentage;
            return _meshCreator.CreatePlanarMesh(vertices, thickness: thickness);
        }

        public MeshDraft CreateWallMeshInfo(
            Vector3 topLeft, Vector3 topRight,
            List<Vector3> doors = null, List<Vector3> windows = null,
            MeshCreateOption option = null)
        {
            option = option == null ? defaultCreateOption : option;

            List<Vector3> vertices = new List<Vector3>();
            List<IList<Vector3>> holes = new List<IList<Vector3>>();

            float thickness = option.thicknessFactor * option.thicknessPercentage;
            var leftToRightDirection = (topRight - topLeft);
            leftToRightDirection.Normalize();

            if (option.stretchWallFollowThickness)
            {
                topRight += leftToRightDirection * (thickness * 0.5f);
                topLeft -= leftToRightDirection * (thickness * 0.5f);
            }
            vertices = PMLib.ApplyHeight(topLeft, topRight, topLeft.Y);

            if (doors != null)
            {
                for (int i = 1; i < doors.Count; i += 2)
                {
                    List<Vector3> hole = new List<Vector3>();
                    Vector3 doorTopLeft = doors[i - 1];
                    Vector3 doorTopRight = doors[i];

                    PMLib.SortByDistanceOnRay(ref doorTopLeft, ref doorTopRight, leftToRightDirection);
                    PMLib.ClippingSegmentBySegment(topLeft, topRight, ref doorTopLeft, ref doorTopRight);

                    float heightOfTop = doorTopLeft.Y * option.doorHeightPercentage;

                    holes.Add(PMLib.ApplyHeight(doorTopRight, doorTopLeft, heightOfTop));
                }
            }

            if (windows != null)
            {
                for (int i = 1; i < windows.Count; i += 2)
                {
                    List<Vector3> hole = new List<Vector3>();
                    Vector3 windowTopLeft = windows[i - 1];
                    Vector3 windowTopRight = windows[i];

                    PMLib.SortByDistanceOnRay(ref windowTopLeft, ref windowTopRight, leftToRightDirection);
                    PMLib.ClippingSegmentBySegment(topLeft, topRight, ref windowTopLeft, ref windowTopRight);

                    float heightOfTop = windowTopLeft.Y * (option.windowYScalePercentage * 1.5f);
                    float heightOfBottom = windowTopLeft.Y * (option.windowYScalePercentage * 0.5f);

                    holes.Add(PMLib.ApplyHeight(windowTopRight, windowTopLeft, heightOfTop, heightOfBottom));
                }
            }

            return _meshCreator.CreatePlanarMesh(vertices, holes, thickness);
        }
    }
}
