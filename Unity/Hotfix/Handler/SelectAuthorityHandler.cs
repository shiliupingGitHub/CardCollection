using Model;


namespace Hotfix
{
    [MessageHandler(Opcode.SelectAuthority)]
    public class SelectAuthorityHandler : AMHandler<SelectAuthority>
    {
        protected override void Run(SelectAuthority message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();

            if(message.PlayerID == gamerComponent.LocalGamer.Id)
            {
                //显示抢地主交互
                uiRoom.GetComponent<UIRoomComponent>().Interaction.StartGrab();
            }
            else
            {
                
            }
        }
    }
}
