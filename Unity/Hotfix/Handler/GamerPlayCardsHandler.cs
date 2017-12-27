using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.GamerPlayCards)]
    public class GamerPlayCardsHandler : AMHandler<GamerPlayCards>
    {
        protected override void Run(GamerPlayCards message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PlayerID);
            if(gamer != null)
            {
                gamer.GetComponent<GamerUIComponent>().ResetPrompt();

                if (gamer.Id == gamerComponent.LocalGamer.Id)
                {
                    InteractionComponent interaction = uiRoom.GetComponent<UIRoomComponent>().Interaction;
                    interaction.Clear();
                    interaction.EndPlay();
                }

                HandCardsComponent handCards = gamer.GetComponent<HandCardsComponent>();
                handCards.PopCards(message.Cards);
            }
        }
    }
}
