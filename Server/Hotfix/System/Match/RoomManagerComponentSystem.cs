using System.Linq;
using System.Collections.Generic;
using Model;

namespace Hotfix
{
    public static class RoomManagerComponentSystem
    {
        public static void Add(this RoomManagerComponent self, Room room)
        {
            self.rooms.Add(room.Id, room);
            self.idleRooms.Enqueue(room);
        }

        public static void Recycle(this RoomManagerComponent self, long id)
        {
            Room room = self.readyRooms[id];
            self.readyRooms.Remove(room.Id);
            self.idleRooms.Enqueue(room);
        }

        public static Room Get(this RoomManagerComponent self, long id)
        {
            Room room;
            self.rooms.TryGetValue(id, out room);
            return room;
        }

        public static Room GetReadyRoom(this RoomManagerComponent self)
        {
            return self.readyRooms.Where(p => p.Value.Count < 3).FirstOrDefault().Value;
        }

        public static Room GetIdleRoom(this RoomManagerComponent self)
        {
            if (self.IdleRoomCount > 0)
            {
                Room room = self.idleRooms.Dequeue();
                self.readyRooms.Add(room.Id, room);
                return room;
            }
            else
            {
                return null;
            }
        }

        public static void RoomStartGame(this RoomManagerComponent self, long id)
        {
            Room room = self.readyRooms[id];
            self.readyRooms.Remove(id);
            self.gameRooms.Add(room.Id, room);
        }

        public static void RoomEndGame(this RoomManagerComponent self, long id)
        {
            Room room = self.gameRooms[id];
            self.gameRooms.Remove(id);
            self.readyRooms.Add(room.Id, room);
        }
    }
}
