using System.Net;

namespace Model
{
	[ObjectEvent]
	public class PlayerEvent : ObjectEvent<Player>, IAwake<long>
	{
		public void Awake(long account)
		{
			this.Get().Awake(account);
		}
	}

	public sealed class Player : Entity
	{
      public  PlayerBaseInfo mBaseInfo = new PlayerBaseInfo();
		
        public IPEndPoint mapServer { get; set; }
        public int RoomId { get; set; }

        public void Awake(long roleId)
		{
			this.mBaseInfo.roleId= roleId;
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