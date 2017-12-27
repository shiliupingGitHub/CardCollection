using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler(Opcode.AuthorityPlayCard)]
    public class AuthorityPlayCardHandler : AMHandler<AuthorityPlayCard>
    {
        protected override void Run(AuthorityPlayCard message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PlayerID);
            if(gamer != null)
            {
                //重置玩家提示
                gamer.GetComponent<GamerUIComponent>().ResetPrompt();

                //当玩家为先手，清空出牌
                if (message.IsFirst)
                {
                    gamer.GetComponent<HandCardsComponent>().ClearPlayCards();
                }

                //显示出牌按钮
                if (gamer.Id == gamerComponent.LocalGamer.Id)
                {
                    InteractionComponent interaction = uiRoom.GetComponent<UIRoomComponent>().Interaction;
                    interaction.IsFirst = message.IsFirst;
                    interaction.StartPlay();
                }
            }
        }
    }
}
