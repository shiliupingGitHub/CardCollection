using System;
using Model;

namespace Hotfix
{
    [MessageHandler(AppType.Match)]
    public class JoinMatch_RTHandler : AMRpcHandler<JoinMatch_RT, JoinMatch_RE>
    {
        protected override async void Run(Session session, JoinMatch_RT message, Action<JoinMatch_RE> reply)
        {
            JoinMatch_RE response = new JoinMatch_RE();
            try
            {
                MatchComponent matchComponent = Game.Scene.GetComponent<MatchComponent>();
                if (matchComponent.Playing.ContainsKey(message.UserID))
                {
                    //重连房间
                    long roomId = matchComponent.Playing[message.UserID];
                    RoomManagerComponent roomManager = Game.Scene.GetComponent<RoomManagerComponent>();
                    Room room = roomManager.Get(roomId);
                    foreach (var gamer in room.GetAll())
                    {
                        if (gamer.UserID == message.UserID)
                        {
                            long pastId = gamer.Id;
                            gamer.Id = message.PlayerID;
                            room.Replace(pastId, gamer);
                            break;
                        }
                    }
                    ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(roomId);
                    actorProxy.Send(new PlayerReconnect() { PlayerID = message.PlayerID, UserID = message.UserID, GateSessionID = message.GateSessionID });

                    response.ActorID = roomId;
                    reply(response);
                    return;
                }

                //创建匹配玩家
                Matcher matcher = EntityFactory.Create<Matcher, long>(message.PlayerID);
                matcher.UserID = message.UserID;
                matcher.GateSessionID = message.GateSessionID;
                matcher.GateAppID = message.GateAppID;

                await matcher.AddComponent<ActorComponent>().AddLocation();
                //加入匹配队列
                Game.Scene.GetComponent<MatcherComponent>().Add(matcher);
                Log.Info($"玩家{message.PlayerID}加入匹配队列");

                response.ActorID = matcher.Id;
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
