using System.Collections.Generic;

namespace Model
{
    public class RoomComponent : Component
    {
        private readonly Dictionary<long, Room> rooms = new Dictionary<long, Room>();

        public void Add(Room room)
        {
            this.rooms.Add(room.Id, room);
        }

        public Room Get(long id)
        {
            Room room;
            this.rooms.TryGetValue(id, out room);
            return room;
        }

        public void Remove(long id)
        {
            Room room = Get(id);
            this.rooms.Remove(id);
            room?.Dispose();
        }

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
