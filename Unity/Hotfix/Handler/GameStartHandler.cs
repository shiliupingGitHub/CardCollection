using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix
{
    [MessageHandler(Opcode.GameStart)]
    public class GameStartHandler : AMHandler<GameStart>
    {
        protected override void Run(GameStart message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            //初始化玩家UI
            foreach (var gamer in gamerComponent.GetAll())
            {
                GamerUIComponent gamerUI = gamer.GetComponent<GamerUIComponent>();
                gamerUI.GameStart();

                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                if (handCards != null)
                {
                    handCards.Reset();
                }
                else
                {
                    handCards = gamer.AddComponent<HandCardsComponent, GameObject>(gamerUI.Panel);
                }

                handCards.Appear();

                if (gamer.Id == gamerComponent.LocalGamer.Id)
                {
                    //本地玩家添加手牌
                    handCards.AddCards(message.GamerCards);
                }
                else
                {
                    //设置其他玩家手牌数
                    handCards.SetHandCardsNum(message.GamerCardsNum[gamer.Id]);
                }
            }

            //显示牌桌UI
            GameObject desk = uiRoom.GameObject.Get<GameObject>("Desk");
            desk.SetActive(true);
            GameObject lordPokers = desk.Get<GameObject>("LordPokers");

            //重置地主牌
            Sprite lordSprite = Resources.Load<GameObject>("UI").Get<GameObject>("Atlas").Get<Sprite>("None");
            for (int i = 0; i < lordPokers.transform.childCount; i++)
            {
                lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordSprite;
            }

            UIRoomComponent uiRoomComponent = uiRoom.GetComponent<UIRoomComponent>();
            //清空选中牌
            uiRoom.GetComponent<UIRoomComponent>().Interaction.Clear();
            //设置初始倍率
            uiRoomComponent.SetMultiples(1);
        }
    }
}
