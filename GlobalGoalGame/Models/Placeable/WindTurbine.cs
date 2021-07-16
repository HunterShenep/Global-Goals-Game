using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Placeable
{
	class WindTurbine : Monetizable
	{
		public float MoneyPerHour { get; set; }
		public int Uuid { get; set; }
		public Vector2 BadLocation { get; set; }
		public Vector2 Location { get; set; }
		public bool Draggable { get; set; }
		public Random Rand { get; set; }

		public Texture2D Texture { get; set; }

		public void Draw(SpriteBatch spriteBatch)
		{
			throw new NotImplementedException();
		}

		public void LoadContent()
		{
			throw new NotImplementedException();
		}

		public void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
