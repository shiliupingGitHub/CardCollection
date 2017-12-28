using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Model
{
    [ObjectEvent]
    public class MatchComponentEvent : ObjectEvent<MatchComponent>, IAwake
    {
        public void Awake()
        {
            this.Get().Awake();
        }
    }

    public class MatchComponent : Component
    {

        public class MatchInfo
        {
            public int RoomId;
            public IPEndPoint Adress;
        }
        QueueDictionary<int, MatchInfo> mMathcInfos = new QueueDictionary<int, MatchInfo>();
        public void Awake()
        {
            
        }

       public MatchInfo CreateRoomInfo()
        {
            Random r = new Random();
            int id = r.Next(100000, 999999);
            while(mMathcInfos.ContainsKey(id))
                id = r.Next(100000, 999999);
            MatchInfo info = new MatchInfo();
            info.RoomId = id;
            List<StartConfig> mapConfigs =  Game.Scene.GetComponent<StartConfigComponent>().MapConfigs;
            int n = RandomHelper.RandomNumber(0, mapConfigs.Count);
            info.Adress = mapConfigs[n].GetComponent<InnerConfig>().IPEndPoint;
            return info;
        }
    }
}
