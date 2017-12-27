using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.Discard)]
    public class DiscardHandler : AMHandler<Discard>
    {
        protected override void Run(Discard message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PlayerID);
            if(gamer != null)
            {
                if(gamer.Id == gamerComponent.LocalGamer.Id)
                {
                    uiRoom.GetComponent<UIRoomComponent>().Interaction.EndPlay();
                }
                gamer.GetComponent<HandCardsComponent>().ClearPlayCards();
                gamer.GetComponent<GamerUIComponent>().SetDiscard();
            }
        }
    }
}
