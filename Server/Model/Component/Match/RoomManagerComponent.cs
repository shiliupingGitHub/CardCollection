using System.Linq;
using System.Collections.Generic;

namespace Model
{
    public class RoomManagerComponent : Component
    {
        public readonly Dictionary<long, Room> rooms = new Dictionary<long, Room>();
        public readonly Dictionary<long, Room> gameRooms = new Dictionary<long, Room>();
        public readonly Dictionary<long, Room> readyRooms = new Dictionary<long, Room>();
        public readonly EQueue<Room> idleRooms = new EQueue<Room>();

        public int TotalCount { get { return this.rooms.Count; } }
        public int GameRoomCount { get { return gameRooms.Count; } }
        public int ReadyRoomCount { get { return readyRooms.Where(p => p.Value.Count < 3).Count(); } }
        public int IdleRoomCount { get { return idleRooms.Count; } }

        public override void Dispose()
        {
            if(this.Id == 0)
            {
                return;
            }

            base.Dispose();

            foreach (var room in this.rooms.Values)
            {
                room.Dispose();
            }
        }
    }
}
