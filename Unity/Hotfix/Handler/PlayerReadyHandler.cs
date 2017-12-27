using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler(Opcode.PlayerReady)]
    public class PlayerReadyHandler : AMHandler<PlayerReady>
    {
        protected override void Run(PlayerReady message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PlayerID);
            gamer.GetComponent<GamerUIComponent>().SetReady();

            //本地玩家准备,隐藏准备按钮
            if (gamer.Id == gamerComponent.LocalGamer.Id)
            {
                uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(false);
            }
        }
    }
}
