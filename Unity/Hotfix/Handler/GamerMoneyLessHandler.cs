using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.GamerMoneyLess)]
    public class GamerMoneyLessHandler : AMHandler<GamerMoneyLess>
    {
        protected override void Run(GamerMoneyLess message)
        {
            //发送退出消息
            long playerId = ClientComponent.Instance.LocalPlayer.Id;
            if (message.PlayerID == playerId)
            {
                SessionComponent.Instance.Session.Send(new Quit() { PlayerID = playerId });
            }

            //切换到大厅界面
            Hotfix.Scene.GetComponent<UIComponent>().Create(UIType.Lobby);
            Hotfix.Scene.GetComponent<UIComponent>().Remove(UIType.Room);
        }
    }
}
