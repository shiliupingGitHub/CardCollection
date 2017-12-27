using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.GamerOut)]
    public class GamerOutHandler : AMHandler<GamerOut>
    {
        protected override void Run(GamerOut message)
        {
            //移除玩家
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            uiRoom.GetComponent<GamerComponent>().Remove(message.PlayerID);
        }
    }
}
