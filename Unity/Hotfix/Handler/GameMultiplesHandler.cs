using Model;

namespace Hotfix
{
    [MessageHandler(Opcode.GameMultiples)]
    public class GameMultiplesHandler : AMHandler<GameMultiples>
    {
        protected override void Run(GameMultiples message)
        {
            UI uiRoom = Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room);
            uiRoom.GetComponent<UIRoomComponent>().SetMultiples(message.Multiples);
        }
    }
}
