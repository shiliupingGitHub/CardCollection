
using Model;
using System;
using System.Net;

namespace Hotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_CreateCowHandler : AMHandler<C2G_CreateCow>
    {
 

        protected override async void Run(Session session, C2G_CreateCow message)
        {
            G2C_RoomCommand response = new G2C_RoomCommand();
            try
            {
                Log.Debug(MongoHelper.ToJson(message));
                Player player = session.GetComponent<SessionPlayerComponent>().Player;
                IPEndPoint matchAddress = Game.Scene.GetComponent<StartConfigComponent>().MatchConfig.GetComponent<InnerConfig>().IPEndPoint;
                Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchAddress);
                Match2G_ObtainId obtainId = await matchSession.Call<Match2G_ObtainId>(new G2Match_ObtainID());
                Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(obtainId.mAdrees);
                M2G_CreateRoom m2g_creteroom = await mapSession.Call<M2G_CreateRoom>(new G2M_CreateRoom() { RoomId = obtainId.RoomId ,GameType = GameType.GT_Cow,playerInfo = player.mBaseInfo });
                player.mapServer = obtainId.mAdrees;
                player.RoomId = obtainId.RoomId;
                response.command = m2g_creteroom.camand;

                session.Send(response);
               // reply(response);

                // 在map服务器上创建战斗Unit
                //IPEndPoint mapAddress = Game.Scene.GetComponent<StartConfigComponent>().MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                //Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                //M2G_CreateUnit createUnit = await mapSession.Call<M2G_CreateUnit>(new G2M_CreateUnit() { PlayerId = player.Id, GateSessionId = session.Id });
                //player.UnitId = createUnit.UnitId;
                //response.UnitId = createUnit.UnitId;
                //response.Count = createUnit.Count;
                //reply(response);
            }
            catch (Exception e)
            {
                
               // ReplyError(response, e, reply);
            }
        }
    }
}
