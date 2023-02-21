using Model.IMDF;
using System.Collections.Generic;
using Tizen.NUI;

namespace ViewModel
{
    public class RoomWall
    {
        public string Id;
        public List<Vector3> Coordinates;
        public List<Opening> Openings;

        public RoomWall(IMDFWall data)
        {
            Coordinates = new List<Vector3>();
            Openings = new List<Opening>();
            Id = data.id;
            foreach (var coordinate in data.coordinates)
            {
                Coordinates.Add(new Vector3(coordinate[0], coordinate[1], coordinate[2]));
            }
        }

        public void AddOpening(IMDFOpening data)
        {
            Openings.Add(new Opening(data));
        }
    }
}
