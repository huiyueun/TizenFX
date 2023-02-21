using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.IMDF
{
    public class IMDFBuilding
    {
        //mandetory
        public string id;
        public string name;
        public IMDFBuildingAddress address;
        public List<string> floors;
        public List<IMDFBuildingImage> images;

        //optional
        public string buildingType;
        public string type;
        public float size;
        public float scale;
    }
}
