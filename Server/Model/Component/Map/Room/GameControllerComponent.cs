namespace Model
{
    public class GameControllerComponent : Component
    {
        public RoomConfig Config { get; set; }

        public long BasePointPerMatch { get; set; }

        public int Multiples { get; set; }

        public long MinThreshold { get; set; }
    }
}
