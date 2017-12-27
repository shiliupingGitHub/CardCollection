using System.Collections.Generic;

namespace Model
{
    public class GateSessionKeyComponent : Component
    {
        private readonly Dictionary<long, long> sessionKey = new Dictionary<long, long>();

        public void Add(long key, long userId)
        {
            this.sessionKey.Add(key, userId);
            this.TimeoutRemoveKey(key);
        }

        public long Get(long key)
        {
            long userId;
            this.sessionKey.TryGetValue(key, out userId);

            //使用或过期之后移除密匙
            if (userId != 0)
            {
                Remove(key);
            }

            return userId;
        }

        public void Remove(long key)
        {
            this.sessionKey.Remove(key);
        }

        private async void TimeoutRemoveKey(long key)
        {
            await Game.Scene.GetComponent<TimerComponent>().WaitAsync(20000);
            //密匙过期向登录服务器发送玩家断开消息
            if (this.sessionKey.ContainsKey(key))
            {
                long userId = Get(key);
                string realmAddress = Game.Scene.GetComponent<StartConfigComponent>().RealmConfig.GetComponent<InnerConfig>().Address;
                Session realmSession = Game.Scene.GetComponent<NetInnerComponent>().Get(realmAddress);
                realmSession.Send(new PlayerDisconnect() { UserID = userId });
            }
        }
    }
}
