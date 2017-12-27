using System;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class GetJoinRoomKey_RTHandler : AMActorRpcHandler<Room, GetJoinRoomKey_RT, GetJoinRoomKey_RE>
    {
        protected override Task Run(Room unit, GetJoinRoomKey_RT message, Action<GetJoinRoomKey_RE> reply)
        {
            GetJoinRoomKey_RE response = new GetJoinRoomKey_RE();
            try
            {
                //创建玩家
                Gamer gamer = EntityFactory.CreateWithId<Gamer, long>(message.PlayerID, message.UserID);
                gamer.AddComponent<UnitGateComponent, long>(message.GateSeesionID);

                //随机密匙
                long key = RandomHelper.RandInt64();
                unit.GetComponent<RoomJoinKeyComponent>().Add(key, gamer);
                Log.Info($"获取进入房间密匙{key}");

                response.Key = key;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
            return Task.CompletedTask;
        }
    }
}
