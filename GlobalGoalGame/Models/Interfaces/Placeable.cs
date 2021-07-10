using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Interfaces
{
	public interface Placeable
	{
		public int Uuid { get; set; }

		public Vector2 BadLocation { get; set; }

		public Vector2 Location { get; set; }

		public bool Draggable { get; set; }

		public Random Rand { get; set; }

		public Texture2D Texture { get; }

		public void LoadContent();

		public void Update(GameTime gameTime);

		public void Draw(SpriteBatch spriteBatch);

	}
}
