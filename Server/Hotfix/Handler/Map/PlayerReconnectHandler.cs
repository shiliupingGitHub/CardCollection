using System.Threading.Tasks;
using Model;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class PlayerReconnectHandler : AMActorHandler<Room, PlayerReconnect>
    {
        protected override Task Run(Room entity, PlayerReconnect message)
        {
            Gamer gamer = entity.GetAll().Where(g => g.UserID == message.UserID).FirstOrDefault();
            if (gamer != null)
            {
                long pastId = gamer.Id;
                gamer.Id = message.PlayerID;
                gamer.isOffline = false;
                entity.Replace(pastId, gamer);

                gamer.RemoveComponent<AutoPlayCardsComponent>();

                UnitGateComponent unitGateComponent = gamer.GetComponent<UnitGateComponent>();
                unitGateComponent.GateSessionId = message.GateSessionID;

                ActorProxy actorProxy = unitGateComponent.GetActorProxy();
                OrderControllerComponent orderController = entity.GetComponent<OrderControllerComponent>();
                DeskCardsCacheComponent deskCardsCache = entity.GetComponent<DeskCardsCacheComponent>();
                GameControllerComponent gameController = entity.GetComponent<GameControllerComponent>();

                //替换过期玩家ID
                if (orderController.FirstAuthority.Key == pastId)
                {
                    orderController.FirstAuthority = new KeyValuePair<long, bool>(gamer.Id, orderController.FirstAuthority.Value);
                }
                if (orderController.Biggest == pastId)
                {
                    orderController.Biggest = gamer.Id;
                }
                if (orderController.CurrentAuthority == pastId)
                {
                    orderController.CurrentAuthority = gamer.Id;
                }

                entity.Broadcast(new GamerReenter() { PastID = pastId, NewID = gamer.Id });

                //发送房间玩家信息
                Gamer[] gamers = entity.GetAll();
                GamerInfo[] gamersInfo = new GamerInfo[gamers.Length];
                for (int i = 0; i < gamers.Length; i++)
                {
                    gamersInfo[i] = new GamerInfo();
                    gamersInfo[i].PlayerID = gamers[i].Id;
                    gamersInfo[i].UserID = gamers[i].UserID;
                    gamersInfo[i].IsReady = gamers[i].IsReady;
                }
                actorProxy.Send(new GamerEnter() { RoomID = entity.Id, GamersInfo = gamersInfo });

                Dictionary<long, int> gamerCardsNum = new Dictionary<long, int>();
                Dictionary<long, Identity> gamersIdentity = new Dictionary<long, Identity>();
                Array.ForEach(gamers, (g) =>
                {
                    HandCardsComponent handCards = g.GetComponent<HandCardsComponent>();
                    gamerCardsNum.Add(g.Id, handCards.CardsCount);
                    gamersIdentity.Add(g.Id, handCards.AccessIdentity);
                });

                //发送玩家手牌
                actorProxy.Send(new GameStart()
                {
                    GamerCards = gamer.GetComponent<HandCardsComponent>().GetAll(),
                    GamerCardsNum = gamerCardsNum
                });

                Card[] lordCards = null;
                if (gamer.GetComponent<HandCardsComponent>().AccessIdentity == Identity.None)
                {
                    //广播先手玩家
                    entity.Broadcast(new SelectAuthority() { PlayerID = orderController.CurrentAuthority });
                }
                else
                {
                    lordCards = deskCardsCache.LordCards.ToArray();
                }

                //发送重连消息
                actorProxy.Send(new GamerReconnect()
                {
                    PlayerID = gamer.Id,
                    Multiples = gameController.Multiples,
                    GamersIdentity = gamersIdentity,
                    LordCards = lordCards,
                    DeskCards = new KeyValuePair<long, Card[]>(orderController.Biggest, deskCardsCache.library.ToArray())
                });

                //发送当前出牌者消息
                bool isFirst = orderController.Biggest == orderController.CurrentAuthority;
                actorProxy.Send(new AuthorityPlayCard() { PlayerID = orderController.CurrentAuthority, IsFirst = isFirst });

                Log.Info($"玩家{gamer.Id}重连");
            }
            return Task.CompletedTask;
        }
    }
}
