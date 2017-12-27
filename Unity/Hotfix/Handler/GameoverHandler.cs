using Model;
using UnityEngine;

namespace Hotfix
{
    [MessageHandler(Opcode.Gameover)]
    public class GameoverHandler : AMHandler<Gameover>
    {
        protected override void Run(Gameover message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Identity localGamerIdentity = gamerComponent.LocalGamer.GetComponent<HandCardsComponent>().AccessIdentity;
            UI uiEndPanel = UIEndFactory.Create(Hotfix.Scene, UIType.EndPanel, uiRoom, message.Winner == localGamerIdentity);
            UIEndComponent endComponent = uiEndPanel.GetComponent<UIEndComponent>();
            uiRoom.Add(uiEndPanel);

            foreach (var gamer in gamerComponent.GetAll())
            {
                gamer.GetComponent<GamerUIComponent>().UpdateInfo();
                gamer.GetComponent<HandCardsComponent>().Hide();
                endComponent.CreateGamerContent(
                    gamer,
                    message.Winner,
                    message.BasePointPerMatch,
                    message.Multiples,
                    message.GamersScore[gamer.Id]);
            }

            UIRoomComponent uiRoomComponent = uiRoom.GetComponent<UIRoomComponent>();
            uiRoomComponent.Interaction.Gameover();
            uiRoomComponent.ResetMultiples();
        }
    }
}
