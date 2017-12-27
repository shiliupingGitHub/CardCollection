namespace Model
{
    [ObjectEvent]
    public class MatcherEvent : ObjectEvent<Matcher>, IAwake<long>
    {
        public void Awake(long id)
        {
            this.Get().Awake(id);
        }
    }

    public sealed class Matcher : Entity
    {
        public long PlayerID { get; private set; }
        public long UserID { get; set; }
        public long GateSessionID { get; set; }
        public int GateAppID { get; set; }

        public void Awake(long id)
        {
            this.PlayerID = id;
        }
    }
}
