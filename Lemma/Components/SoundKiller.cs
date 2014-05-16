﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
using Microsoft.Xna.Framework;
using ComponentBind;

namespace Lemma.Components
{
	public class SoundKiller : Component<Main>
	{
		public ListProperty<uint> Events = new ListProperty<uint>();
		public override void Awake()
		{
			base.Awake();
			this.Serialize = false;

			this.Add(new CommandBinding(this.Delete, delegate()
			{
				foreach (uint e in this.Events)
					AkSoundEngine.PostEvent(e, this.Entity);
			}));
		}

		public static void Add(Entity result, params string[] events)
		{
			SoundKiller.Add(result, events.Select(x => AkSoundEngine.GetIDFromString(x)).ToArray());
		}

		public static void Add(Entity result, params uint[] events)
		{
			SoundKiller killer = result.GetOrCreate<SoundKiller>();
			foreach (uint e in events)
				killer.Events.Add(e);
		}
	}
}
