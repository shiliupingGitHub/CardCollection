using System.Collections.Generic;

namespace Model
{
    public class RoomJoinKeyComponent : Component
    {
        private readonly Dictionary<long, Gamer> keys = new Dictionary<long, Gamer>();

        public void Add(long key,Gamer gamer)
        {
            this.keys.Add(key, gamer);
            JoinTimeout(key);
        }

        public Gamer Get(long key)
        {
            Gamer gamer;
            this.keys.TryGetValue(key, out gamer);

            //使用或过期后移除密匙
            if(gamer != null)
            {
                this.keys.Remove(key);
            }

            return gamer;
        }

        private async void JoinTimeout(long key)
        {
            await Game.Scene.GetComponent<TimerComponent>().WaitAsync(3000);
            if (this.keys.ContainsKey(key))
            {
                Gamer gamer = Get(key);

                //向匹配服务器发送玩家离开房间消息
                string matchAddress = Game.Scene.GetComponent<StartConfigComponent>().MatchConfig.GetComponent<InnerConfig>().Address;
                Session matchSession = Game.Scene.GetComponent<NetInnerComponent>().Get(matchAddress);
                matchSession.Send(new GamerQuitRoom() { PlayerID = gamer.Id, RoomID = this.GetEntity<Room>().Id });

                gamer.Dispose();
            }
        }
    }
}
