using Model.IMDF;
using System.Collections.Generic;
using Tizen.NUI;

namespace ViewModel
{
    public class Opening
    {
        public string Id;
        public OpeningCategory Category;
        public List<Vector3> Coordinates;
        public bool Visible;

        public Opening(IMDFOpening data)
        {
            Id = data.id;
            if (data.properties.category.ToLower() == "window")
            {
                Category = OpeningCategory.WINDOW;
            }
            if (data.properties.category.ToLower() == "door")
            {
                Category = OpeningCategory.DOOR;
            }
            Coordinates = new List<Vector3>();
            foreach (var coordinate in data.coordinates)
            {
                Coordinates.Add(new Vector3(coordinate[0], coordinate[1], coordinate[2]));
            }
            Visible = data.properties.visible;
        }
    }
}
