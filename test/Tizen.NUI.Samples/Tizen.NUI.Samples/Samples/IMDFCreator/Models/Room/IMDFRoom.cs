using System.Collections.Generic;

namespace Model.IMDF
{
    public class IMDFRoom
    {
        public string id;
        public IMDFRoomProperty properties;
        public IMDFPlane plane;
        public List<IMDFWall> walls;
        public IMDFRoomSmartThings smartthings;
    }
}
