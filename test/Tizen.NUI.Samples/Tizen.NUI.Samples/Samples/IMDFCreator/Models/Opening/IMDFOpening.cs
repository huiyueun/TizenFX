using System.Collections.Generic;

namespace Model.IMDF
{
    public class IMDFOpening
    {
        public string id;
        public string floor;
        public IMDFOpeningProperty properties;
        public List<List<float>> coordinates;
    }
}
