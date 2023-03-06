using System.Collections.Generic;
using ViewModel;

namespace Tizen.NUI.Samples
{ 
    public class RoomView : IMDF3DView
    {
        public RoomView(Room roomData)
        {
            Tizen.Log.Info("MYLOG", "room id : " + roomData.Id + "\n");
            Size = new Size(300, 300, 300);
            Position = new Position(0, 0, 0);
            CreateWallAndPlane(roomData);
        }

        private void CreateWallAndPlane(Room roomData)
        {
            CreatePlane(roomData.Category, roomData.Plane);
            CreateWall(roomData.Walls);
        }

        private void CreateWall(List<RoomWall> walls)
        {
            Tizen.Log.Error("MYLOG", "WallCount : " + walls.Count + "\n");
            foreach (var wall in walls)
            {
                var wallView = new WallView(wall);
                Add(wallView);
            }
        }

        private void CreatePlane(RoomCategory category, RoomPlane data)
        {
            var planeView = new PlaneView(category, data);
            Add(planeView);
        }
    }
}
