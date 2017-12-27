using Model;

namespace Hotfix
{
    [Event((int)EventIdType.CancelHandCard)]
    public class CancelHandCardEvent : IEvent<Card>
    {
        public void Run(Card card)
        {
            Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room).GetComponent<UIRoomComponent>().Interaction.CancelCard(card);
        }
    }
}
