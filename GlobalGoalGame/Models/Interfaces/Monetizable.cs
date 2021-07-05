using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	interface Monetizable
	{
		public int Uuid{ get; set; }

		public float MoneyPerHour { get; set; }

		public float Cost { get; set; }

		public Random Rand { get; set; }

		public static List<Texture2D> Textures { get; set; }

		public Texture2D Texture { get; }

		public void LoadContent();

		public void Update(GameTime gameTime);

		public void Draw(SpriteBatch spriteBatch);
		




	}
}
