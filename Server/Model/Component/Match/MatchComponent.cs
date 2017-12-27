using System.Collections.Generic;

namespace Model
{
    public class MatchComponent : Component
    {
        public readonly Dictionary<long, long> Playing = new Dictionary<long, long>();
        public readonly EQueue<Matcher> MatchSuccessQueue = new EQueue<Matcher>();
        public bool CreateRoomLock { get; set; }
    }
}
