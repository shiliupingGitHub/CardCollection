using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Map)]
    public class M2G_CreateRoomHandler : AMRpcHandler<G2M_CreateRoom, M2G_CreateRoom>
    {
        protected override  void Run(Session session, G2M_CreateRoom message, Action<M2G_CreateRoom> reply)
        {
            M2G_CreateRoom response = new M2G_CreateRoom();
            try
            {
                Game.Scene.GetComponent<RoomComponent>().CreateRoom(message.RoomId, message.GameType,message.playerInfo);
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}