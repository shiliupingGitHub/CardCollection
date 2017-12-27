namespace Model
{
    [ObjectEvent]
    public class PlayerEvent : ObjectEvent<Player>, IAwake<long>
    {
        public void Awake(long id)
        {
            this.Get().Awake(id);
        }
    }

    public sealed class Player : Entity
    {
        public long UserID { get; private set; }

        public long ActorID { get; set; }

        public void Awake(long id)
        {
            this.UserID = id;
        }

        public override void Dispose()
        {
            if (this.Id == 0)
            {
                return;
            }

            base.Dispose();
        }
    }
}