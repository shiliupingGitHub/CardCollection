using System;
using System.Threading.Tasks;
using Model;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class PlayerJoinRoom_RTHandler : AMActorRpcHandler<Room, PlayerJoinRoom_RT, PlayerJoinRoom_RE>
    {
        protected override Task Run(Room unit, PlayerJoinRoom_RT message, Action<PlayerJoinRoom_RE> reply)
        {
            PlayerJoinRoom_RE response = new PlayerJoinRoom_RE();

            try
            {
                Gamer gamer = unit.GetComponent<RoomJoinKeyComponent>().Get(message.Key);

                //验证密匙
                if (gamer != null)
                {
                    unit.Add(gamer);

                    //广播消息
                    Gamer[] gamers = unit.GetAll();
                    GamerInfo[] gamersInfo = new GamerInfo[gamers.Length];
                    for (int i = 0; i < gamers.Length; i++)
                    {
                        gamersInfo[i] = new GamerInfo();
                        gamersInfo[i].PlayerID = gamers[i].Id;
                        gamersInfo[i].UserID = gamers[i].UserID;
                        gamersInfo[i].IsReady = gamers[i].IsReady;
                    }
                    unit.Broadcast(new GamerEnter() { RoomID = unit.Id, GamersInfo = gamersInfo });
                    Log.Info($"玩家{gamer.Id}进入房间");
                }
                else
                {
                    Log.Info($"玩家进入房间验证失败，密匙：{message.Key}");
                    response.Error = ErrorCode.ERR_JoinRoomError;
                }

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
