﻿using System; using ComponentBind;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Lemma.Components;
using System.IO;
using System.Xml.Serialization;
using Lemma.Util;

namespace Lemma.Factories
{
	public class PlayerDataFactory : Factory<Main>
	{
		private Entity instance;

		public PlayerDataFactory()
		{
			this.Color = new Vector3(1.0f, 0.4f, 0.4f);
			this.EditorCanSpawn = false;
		}

		public override Entity Create(Main main)
		{
			Entity result = new Entity(main, "PlayerData");

			const bool enabled = true;

			result.Add("EnableRoll", new Property<bool> { Value = enabled });
			result.Add("EnableCrouch", new Property<bool> { Value = enabled });
			result.Add("EnableKick", new Property<bool> { Value = enabled });
			result.Add("EnableWallRun", new Property<bool> { Value = enabled });
			result.Add("EnableWallRunHorizontal", new Property<bool> { Value = enabled });
			result.Add("EnableEnhancedWallRun", new Property<bool> { Value = enabled });
			result.Add("EnableSlowMotion", new Property<bool> { Value = enabled });
			result.Add("EnableStamina", new Property<bool> { Value = enabled });
			result.Add("EnableMoves", new Property<bool> { Value = true });
			result.Add("EnablePhone", new Property<bool> { Value = enabled });
			result.Add("MaxSpeed", new Property<float> { Value = Character.DefaultMaxSpeed, Editable = false });
			result.Add("GameTime", new Property<float> { Editable = false });
			result.Add("Phone", new Phone());

			return result;
		}

		public override void Bind(Entity result, Main main, bool creating = false)
		{
			base.Bind(result, main, creating);
			this.instance = result;

			result.CannotSuspend = true;

			Property<float> gameTime = result.GetOrMakeProperty<float>("GameTime", false);
			result.Add(new Updater
			{
				delegate(float dt)
				{
					gameTime.Value += dt;
				}
			});
		}

		public Entity Instance
		{
			get
			{
				if (this.instance != null && !this.instance.Active)
					this.instance = null;
				return this.instance;
			}
		}

		public override void AttachEditorComponents(Entity result, Main main)
		{
			result.Delete.Execute();
		}
	}
}
