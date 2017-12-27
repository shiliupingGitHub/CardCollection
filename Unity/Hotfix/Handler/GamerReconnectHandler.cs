using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix
{
    [MessageHandler(Opcode.GamerReconnect)]
    public class GamerReconnectHandler : AMHandler<GamerReconnect>
    {
        protected override void Run(GamerReconnect message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            if (message.PlayerID == gamerComponent.LocalGamer.Id)
            {
                uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(false);
                foreach (var gamer in gamerComponent.GetAll())
                {
                    //初始化玩家身份
                    Identity gamerIdentity = message.GamersIdentity[gamer.Id];
                    HandCardsComponent gamerHandCards = gamer.GetComponent<HandCardsComponent>();
                    gamerHandCards.AccessIdentity = gamerIdentity;
                    gamer.GetComponent<GamerUIComponent>().SetIdentity(gamerIdentity);
                    //初始化出牌
                    if (message.DeskCards.Key == gamer.Id && gamerIdentity != Identity.None)
                    {
                        gamerHandCards.PopCards(message.DeskCards.Value);
                    }
                }
            }

            //初始化界面
            UIRoomComponent uiRoomComponent = uiRoom.GetComponent<UIRoomComponent>();
            uiRoomComponent.SetMultiples(message.Multiples);
            uiRoomComponent.Interaction.GameStart();

            //初始化地主牌
            if (message.LordCards != null)
            {
                GameObject lordPokers = uiRoom.GameObject.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
                for (int i = 0; i < lordPokers.transform.childCount; i++)
                {
                    Sprite lordCardSprite = Resources.Load<GameObject>("UI").Get<GameObject>("Atlas").Get<Sprite>(message.LordCards[i].GetName());
                    lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordCardSprite;
                }
            }
        }
    }
}
