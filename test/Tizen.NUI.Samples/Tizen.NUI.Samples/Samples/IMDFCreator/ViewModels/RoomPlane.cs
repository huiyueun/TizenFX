using Model.IMDF;
using System.Collections.Generic;
using Tizen.NUI;

namespace ViewModel
{
    public class RoomPlane
    {
        public string Id;
        public List<Vector3> Coordinates;

        public RoomPlane(IMDFPlane data)
        {
            Coordinates = new List<Vector3>();
            Id = data.id;
            foreach (var coordinate in data.coordinates)
            {
                Coordinates.Add(new Vector3(coordinate[0], coordinate[1], coordinate[2]));
            }
        }
    }
}
