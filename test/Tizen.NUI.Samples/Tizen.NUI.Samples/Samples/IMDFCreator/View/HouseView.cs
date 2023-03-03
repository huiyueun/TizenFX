using Tizen.NUI.BaseComponents;
using ViewModel;

namespace Tizen.NUI.Samples
{
    public class HouseView : IMDF3DView
    {
        public HouseView(Building data)
        {
            Size = new Size(300, 300, 300);
            Position = new Position(0, 0, 0);

            CreateRooms(data);
        }

        public void CreateRooms(Building data)
        {
            Tizen.Log.Info("MYLOG", "Room Count : " + data.Rooms.Count +"\n");
            int idx = 0;
            foreach (var room in  data.Rooms)
            {
                var roomView = new RoomView(room);
                Add(roomView);
                idx++;
            }
        }
    }
}
