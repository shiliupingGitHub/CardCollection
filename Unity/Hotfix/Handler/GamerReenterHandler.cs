using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.GamerReenter)]
    public class GamerReenterHandler : AMHandler<GamerReenter>
    {
        protected override void Run(GamerReenter message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            GamerComponent gamerComponent = uiRoom.GetComponent<GamerComponent>();
            Gamer gamer = gamerComponent.Get(message.PastID);
            if(gamer != null)
            {
                gamer.Id = message.NewID;
                gamerComponent.Replace(message.PastID, gamer);
            }
        }
    }
}
