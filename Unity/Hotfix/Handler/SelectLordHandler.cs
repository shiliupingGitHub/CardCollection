using Model;
using UnityEngine;
using UnityEngine.UI;

namespace Hotfix
{
    [MessageHandler(Opcode.SelectLord)]
    public class SelectLordHandler : AMHandler<SelectLord>
    {
        protected override void Run(SelectLord message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PlayerID);

            if (gamer != null)
            {
                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                if (gamer.Id == gamerComponent.LocalGamer.Id)
                {
                    //本地玩家添加手牌
                    handCards.AddCards(message.LordCards);
                }
                else
                {
                    //其他玩家设置手牌数
                    handCards.SetHandCardsNum(20);
                }
            }

            foreach (var _gamer in gamerComponent.GetAll())
            {
                if(_gamer.Id == message.PlayerID)
                {
                    _gamer.GetComponent<HandCardsComponent>().AccessIdentity = Identity.Landlord;
                    _gamer.GetComponent<GamerUIComponent>().SetIdentity(Identity.Landlord);
                }
                else
                {
                    _gamer.GetComponent<HandCardsComponent>().AccessIdentity = Identity.Farmer;
                    _gamer.GetComponent<GamerUIComponent>().SetIdentity(Identity.Farmer);
                }
            }

            //重置玩家UI提示
            foreach (var _gamer in gamerComponent.GetAll())
            {
                _gamer.GetComponent<GamerUIComponent>().ResetPrompt();
            }

            //切换地主牌精灵
            GameObject lordPokers = uiRoom.GameObject.Get<GameObject>("Desk").Get<GameObject>("LordPokers");
            for (int i = 0; i < lordPokers.transform.childCount; i++)
            {
                Sprite lordCardSprite = Resources.Load<GameObject>("UI").Get<GameObject>("Atlas").Get<Sprite>(message.LordCards[i].GetName());
                lordPokers.transform.GetChild(i).GetComponent<Image>().sprite = lordCardSprite;
            }

            //显示切换游戏模式按钮
            uiRoom.GetComponent<UIRoomComponent>().Interaction.GameStart();
        }
    }
}
