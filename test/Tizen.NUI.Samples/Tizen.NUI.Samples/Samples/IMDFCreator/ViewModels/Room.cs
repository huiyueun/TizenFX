using Model.IMDF;
using System;
using System.Collections.Generic;

namespace ViewModel
{
    public class Room
    {
        public string WallId;
        public string RoomPlaneId;

        public string Id;
        public RoomCategory Category = RoomCategory.NONE;

        public string Name;
        public bool Visible;
        public RoomPlane Plane;
        public List<RoomWall> Walls;
        public string SmartThingsRoomId;

        public Room(IMDFRoom data)
        {
            Walls = new List<RoomWall>();
            Id = data.id;

            foreach (RoomCategory type in Enum.GetValues(typeof(RoomCategory)))
            {
                if (type.ToString() == data.properties.category.ToUpper())
                {
                    Category = type;
                    break;
                }
            }

            Name = data.properties.name;
            Visible = data.properties.visible;
            SmartThingsRoomId = data.smartthings?.id;

            Plane = new RoomPlane(data.plane);
            foreach (var wall in data.walls)
            {
                Walls.Add(new RoomWall(wall));

            }
        }

        public void SetOpeningData(List<IMDFOpening> data)
        {
            foreach (var opening in data)
            {
                foreach (var wall in Walls)
                {
                    if (opening.properties.groups.Contains(wall.Id))
                    {
                        wall.AddOpening(opening);
                    }
                }
            }
        }
    }
}
