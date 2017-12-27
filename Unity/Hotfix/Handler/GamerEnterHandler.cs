using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler(Opcode.GamerEnter)]
    public class GamerEnterHandler : AMHandler<GamerEnter>
    {
        protected override void Run(GamerEnter message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            //隐藏匹配提示
            GameObject matchPrompt = uiRoom.GameObject.Get<GameObject>("MatchPrompt");
            if (matchPrompt.activeSelf)
            {
                matchPrompt.SetActive(false);
                uiRoom.GameObject.Get<GameObject>("ReadyButton").SetActive(true);
            }

            //添加未显示玩家
            for (int i = 0; i < message.GamersInfo.Length; i++)
            {
                GamerInfo info = message.GamersInfo[i];
                if (gamerComponent.Get(info.PlayerID) == null)
                {
                    Gamer gamer = EntityFactory.CreateWithId<Gamer, long>(info.PlayerID, info.UserID);
                    gamer.IsReady = info.IsReady;
                    gamer.AddComponent<GamerUIComponent, UI>(uiRoom);
                    gamerComponent.Add(gamer);
                }
            }
        }
    }
}
