using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.GrabLordSelect)]
    public class GrabLordSelectHandler : AMHandler<GrabLordSelect>
    {
        protected override void Run(GrabLordSelect message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PlayerID);
            if(gamer != null)
            {
                if(gamer.Id == gamerComponent.LocalGamer.Id)
                {
                    uiRoom.GetComponent<UIRoomComponent>().Interaction.EndGrab();
                }
                gamer.GetComponent<GamerUIComponent>().SetGrab(message.IsGrab);
            }
        }
    }
}
