using System.Collections.Generic;

namespace Model.IMDF
{
    public class IMDFIndoorMap
    {
        public string id;
        public IMDFBuilding building;
        public List<IMDFFloor> floors;
    }
}
