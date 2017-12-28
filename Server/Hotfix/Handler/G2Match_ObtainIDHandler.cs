
using Model;
using System;
using System.Net;

namespace Hotfix
{
    [MessageHandler(AppType.Match)]
    public class G2Match_ObtainIDHandler : AMRpcHandler<G2Match_ObtainID, Match2G_ObtainId>
    {
        protected override  void Run(Session session, G2Match_ObtainID message, Action<Match2G_ObtainId> reply)
        {
            Match2G_ObtainId response = new Match2G_ObtainId();
            try
            {
                MatchComponent.MatchInfo mi = Game.Scene.GetComponent<MatchComponent>().CreateRoomInfo();
                 response.RoomId = mi.RoomId;
                response.mAdrees = mi.Adress;
                reply(response);


            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }


    }
}
