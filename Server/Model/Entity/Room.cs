using System.Net;
using System.Collections.Generic;
namespace Model
{
    [ObjectEvent]
    public class RoomEvent : ObjectEvent<Room>, IAwake<int, PlayerBaseInfo>
    {
        public void Awake(int id, PlayerBaseInfo info)
        {
            this.Get().Awake(id,info);
        }
    }

    public sealed class Room : Entity
    {
        public int id { get; private set; }
        public PlayerBaseInfo mOwnerInfo;
        public Dictionary<long, PlayerBaseInfo> mInfo = new Dictionary<long, PlayerBaseInfo>();

        public void Awake(int id, PlayerBaseInfo info)
        {
            this.id = id;
            this.mOwnerInfo = info;
            mInfo[info.roleId] = info;
        }

        public override void Dispose()
        {
            if (this.Id == 0)
            {
                return;
            }

            base.Dispose();
        }
    }
}
