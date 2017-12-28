﻿using System.Net;

namespace Model
{
	[ObjectEvent]
	public class PlayerEvent : ObjectEvent<Player>, IAwake<string>
	{
		public void Awake(string account)
		{
			this.Get().Awake(account);
		}
	}

	public sealed class Player : Entity
	{
		public string Account { get; private set; }
		
		public long UnitId { get; set; }
        public IPEndPoint mapServer { get; set; }

        public void Awake(string account)
		{
			this.Account = account;
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