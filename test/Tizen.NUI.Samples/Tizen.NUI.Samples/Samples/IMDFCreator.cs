using Model.IMDF;
using System.Collections.Generic;
using System.Linq;
using Tizen.NUI.BaseComponents;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class IMDFCreator : IExample
    {
        private Window win;
        private View root;
        private Animation rotateAnimation;
        private bool isIMDFDataStorageReady;

        public void Activate()
        {
            win = NUIApplication.GetDefaultWindow();

            // Set layer behavior as Layer3D. without this, Rendering will be broken
            win.GetDefaultLayer().Behavior = Layer.LayerBehavior.Layer3D;
            root = new View()
            {
                Name = "root",
                BackgroundColor = new Color(0.2f, 0.2f, 0.2f, 1.0f),
                WidthResizePolicy = ResizePolicyType.FillToParent,
                HeightResizePolicy = ResizePolicyType.FillToParent,
            };
            win.Add(root);

            LoadIMDFModel();
        }

        public void Deactivate()
        {
            RemoveAllViews();
            rotateAnimation?.Dispose();
            root.Unparent();
            root.Dispose();

            // Revert default layer behavior as LayerUI
            win.GetDefaultLayer().Behavior = Layer.LayerBehavior.LayerUI;
        }

        private void LoadIMDFModel()
        {
            isIMDFDataStorageReady = IMDFModel.Instance.IsDataInitialized;

            if (isIMDFDataStorageReady)
            {
                CreateHouse();
            }
            else
            {
                Tizen.Log.Error("MYLOG", $"LoadIMDFModel() : Failed to load json file");
            }
        }

        private void CreateHouse()
        {
            var houseData = GetHouseData();
            FlipZCoodinate(ref houseData);

            var spaceView = new SpaceView(houseData);
            root.Add(spaceView);

            PlayRotateAnimation(20000, spaceView);
        }

        private void FlipZCoodinate(ref Building building)
        {
            var coordinatesList = GetCoordinatesOfBuilding(building);
            float maxZ = coordinatesList.Max((coords) => coords.Max((vec) => vec.Z));

            foreach (var coordinates in coordinatesList)
            {
                for (int i = 0; i < coordinates.Count; i++)
                {
                    coordinates[i] = new Vector3(coordinates[i].X, coordinates[i].Y, maxZ - coordinates[i].Z);
                }
            }
        }

        private List<List<Vector3>> GetCoordinatesOfBuilding(Building building)
        {
            List<List<Vector3>> coordinatesList = new List<List<Vector3>>();

            foreach (var room in building.Rooms)
            {
                coordinatesList.Add(room.Plane.Coordinates);
                foreach (var wall in room.Walls)
                {
                    coordinatesList.Add(wall.Coordinates);

                    foreach (var opening in wall.Openings)
                    {
                        coordinatesList.Add(opening.Coordinates);
                    }
                }
            }

            return coordinatesList;
        }

        private Building GetHouseData()
        {
            if (!IMDFModel.Instance.IsDataInitialized) return null;

            var building = new Building(IMDFModel.Instance.IndoorMap.building);
            building.Create(IMDFModel.Instance.IndoorMap.floors.FirstOrDefault().rooms, IMDFModel.Instance.IndoorMap.floors.FirstOrDefault().openings);

            return building;
        }

        private void PlayRotateAnimation(int millsec, View view)
        {
            view.Position = new Position(0, 0, 500);
            view.RotateBy(new Radian(new Degree(50.0f)), Vector3.XAxis);
            view.RotateBy(new Radian(new Degree(20.0f)), Vector3.ZAxis);

            //view.PivotPoint = new Position(0.5f, 0.5f, 1.0f);
            rotateAnimation = new Animation(millsec); //1.5s
            rotateAnimation.AnimateBy(view, "Orientation", new Rotation(new Radian(new Degree(360.0f)), Vector3.ZAxis));
            rotateAnimation.Looping = true;
            rotateAnimation.Play();
        }

        private void RemoveAllViews()
        {
            var cnt = root.ChildCount;
            for (int i = (int)(cnt - 1); i >= 0; i--)
            {
                root.Remove(root.GetChildAt((uint)i));
            }
            rotateAnimation.Clear();
        }
    }
}
