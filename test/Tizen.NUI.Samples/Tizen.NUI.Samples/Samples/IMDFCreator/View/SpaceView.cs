using Tizen.NUI.BaseComponents;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class SpaceView : IMDF3DView
    {
        public SpaceView(Building data)
        {
            Size = new Size(300, 300, 300);
            Position = new Position(0, 0, 0);

            CreateHouse(data);
        }

        private void CreateHouse(Building data)
        {
            var houseView = new HouseView(data);
            Add(houseView);
        }
    }
}
