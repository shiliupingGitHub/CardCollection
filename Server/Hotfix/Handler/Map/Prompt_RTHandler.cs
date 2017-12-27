using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using System.Linq;

namespace Hotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Prompt_RTHandler : AMActorRpcHandler<Room, Prompt_RT, Prompt_RE>
    {
        protected override async Task Run(Room unit, Prompt_RT message, Action<Prompt_RE> reply)
        {
            Prompt_RE response = new Prompt_RE();
            try
            {
                Gamer gamer = unit.Get(message.PlayerID);
                if (gamer != null)
                {
                    List<Card> handCards = new List<Card>(gamer.GetComponent<HandCardsComponent>().GetAll());
                    CardsHelper.SortCards(handCards);
                    if (gamer.Id == unit.GetComponent<OrderControllerComponent>().Biggest)
                    {
                        response.Cards = handCards.Where(card => card.CardWeight == handCards[handCards.Count - 1].CardWeight).ToArray();
                    }
                    else
                    {
                        DeskCardsCacheComponent deskCardsCache = unit.GetComponent<DeskCardsCacheComponent>();
                        List<Card[]> result = await CardsHelper.GetPrompt(handCards, deskCardsCache, deskCardsCache.Rule);

                        if (result.Count > 0)
                        {
                            response.Cards = result[RandomHelper.RandomNumber(0, result.Count)];
                        }
                    }
                }
                reply(response);
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}
