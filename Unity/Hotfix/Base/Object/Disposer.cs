﻿using System;
using Model;

namespace Hotfix
{
	public abstract class Disposer : Object, IDisposable
	{
		public long Id { get; set; }

		public bool IsFromPool { get; set; }

		protected Disposer()
		{
			this.Id = IdGenerater.GenerateId();
		}

		protected Disposer(long id)
		{
			this.Id = id;
		}

		public virtual void Dispose()
		{
			this.Id = 0;
			if (this.IsFromPool)
			{
				ObjectPool.Instance.Recycle(this);
			}
		}
	}
}