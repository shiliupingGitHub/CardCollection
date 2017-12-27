using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public enum RoomState
    {
        Idle,
        Ready,
        Game
    }

    public sealed class Room : Entity
    {
        private readonly Dictionary<long, Gamer> gamers = new Dictionary<long, Gamer>();
        private readonly List<Gamer> gamerList = new List<Gamer>();
        public RoomState State { get; set; } = RoomState.Idle;
        public int Count { get { return gamers.Values.Count; } }

        public void Add(Gamer gamer)
        {
            gamers.Add(gamer.Id, gamer);
            gamerList.Add(gamer);
        }

        public void Replace(long id, Gamer newGamer)
        {
            int index = gamerList.IndexOf(gamers[id]);
            gamerList[index] = newGamer;
            gamers.Remove(id);
            gamers.Add(newGamer.Id, newGamer);
        }

        public Gamer Get(long id)
        {
            Gamer gamer;
            gamers.TryGetValue(id, out gamer);
            return gamer;
        }

        public Gamer[] GetAll()
        {
            return gamerList.ToArray();
        }

        public void Remove(long id)
        {
            gamerList.Remove(gamers[id]);
            gamers.Remove(id);
        }

        public void Broadcast(AMessage message)
        {
            foreach (var gamer in gamers.Values)
            {
                if (gamer.isOffline)
                {
                    continue;
                }
                ActorProxy actorProxy = gamer.GetComponent<UnitGateComponent>().GetActorProxy();
                actorProxy.Send(message);
            }
        }
    }
}
