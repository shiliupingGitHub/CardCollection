using Model;

namespace Hotfix
{
    [ObjectEvent]
    public class AutoPlayCardsComponentEvent : ObjectEvent<AutoPlayCardsComponent>, IAwake<Room>, IUpdate
    {
        public void Awake(Room room)
        {
            this.Get().Awake(room);
        }

        public void Update()
        {
            this.Get().Update();
        }
    }

    public static class AutoPlayCardsComponentSystem
    {
        public static void Awake(this AutoPlayCardsComponent self, Room room)
        {
            self.GamerRoom = room;
        }

        public static async void Update(this AutoPlayCardsComponent self)
        {
            if (!self.Playing)
            {
                OrderControllerComponent orderController = self.GamerRoom.GetComponent<OrderControllerComponent>();
                if (self.Entity.Id == orderController.CurrentAuthority)
                {
                    self.Playing = true;
                    await Game.Scene.GetComponent<TimerComponent>().WaitAsync(1000);

                    ActorProxy actorProxy = Game.Scene.GetComponent<ActorProxyComponent>().Get(self.GamerRoom.Id);
                    //当还没抢地主时随机抢地主
                    if (self.Entity.GetComponent<HandCardsComponent>().AccessIdentity == Identity.None)
                    {
                        int randomSelect = RandomHelper.RandomNumber(0, 2);
                        actorProxy.Send(new GrabLordSelect() { PlayerID = self.Entity.Id, IsGrab = randomSelect == 0 });
                        self.Playing = false;
                        return;
                    }

                    Prompt_RE promptRE = await actorProxy.Call<Prompt_RE>(new Prompt_RT() { PlayerID = self.Entity.Id });
                    if (promptRE.Error > 0 || promptRE.Cards == null)
                    {
                        actorProxy.Send(new Discard() { PlayerID = self.Entity.Id });
                    }
                    else
                    {
                        await actorProxy.Call<PlayCards_RE>(new PlayCards_RT() { PlayerID = self.Entity.Id, Cards = promptRE.Cards });
                    }
                    self.Playing = false;
                }
            }
        }
    }
}
