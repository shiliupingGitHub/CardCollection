using Model;

namespace Hotfix
{
    [Event((int)EventIdType.SelectHandCard)]
    public class SelectHandCardEvent : IEvent<Card>
    {
        public void Run(Card card)
        {
            Hotfix.Scene.GetComponent<UIComponent>().Get(UIType.Room).GetComponent<UIRoomComponent>().Interaction.SelectCard(card);
        }
    }
}
