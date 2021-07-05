using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;

namespace GlobalGoalGame
{
	class Trash
	{
		private List<Texture2D> textures { get; set; }
		public Texture2D Texture { get; set; }
		public Vector2 Location;



		public void Update(GameTime gameTime)
		{

		}
		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, Location, Color.White);

		}
	}
}