using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
   public class RoomComponent: Component
    {
        Dictionary<int, Room> mRooms = new Dictionary<int, Room>();
        public void CreateRoom(int id,int gameType, PlayerBaseInfo info)
        {
            EntityFactory.Create<Room,int, PlayerBaseInfo>(id,info);
        }
    }
}
