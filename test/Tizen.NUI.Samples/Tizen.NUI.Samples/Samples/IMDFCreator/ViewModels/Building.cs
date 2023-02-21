using Model.IMDF;
using System;
using System.Collections.Generic;

namespace ViewModel
{
    public class Building
    {
        public List<Room> Rooms;
        public string Id;
        public string Type;
        public BuildingType Category = BuildingType.NONE;
        public float Size;

        public Building(IMDFBuilding data)
        {
            Rooms = new List<Room>();
            Id = data.id;
            Type = data.type;

            if (data.buildingType != null)
            {
                foreach (BuildingType type in Enum.GetValues(typeof(BuildingType)))
                {
                    if (type.ToString() == data.buildingType.ToUpper())
                    {
                        Category = type;
                        break;
                    }
                }
            }

            Size = data.size;
        }

        public void Create(List<IMDFRoom> roomData, List<IMDFOpening> openingData = null)
        {
            Rooms = new List<Room>();
            foreach (var data in roomData)
            {
                Room room = new Room(data);
                if (openingData != null)
                {
                    room.SetOpeningData(openingData);
                }

                Rooms.Add(room);
            }
        }
    }
}
