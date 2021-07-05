using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

using System.Text;

namespace GlobalGoalGame
{
	class TrashSprite
	{
		public Texture2D Texture { get; }
		public Vector2 BadLocation { get; }
		public Vector2 Location { get; }
		public float Value { get; }

		public bool Exists { get; set; }



		private SpriteBatch _spriteBatch;
		
		// The trash 
		public static List<TrashSprite> TheTrash = new List<TrashSprite>();

		Random rand = new Random();

		public TrashSprite(List<Texture2D> theTextures)
		{
			Texture = theTextures[rand.Next(0, theTextures.Count)];
			Value = (float)(rand.Next(0, 10) * 0.01);

			int x = rand.Next(50, (Game1.GameWidth - 50));
			int y = rand.Next(50, (Game1.GameHeight - 50));
			BadLocation = new Vector2(x, y);
			Location = new Vector2(BadLocation.X + 15, BadLocation.Y + 15);


			Exists = true;
		}


		public void Update(GameTime gameTime)
		{

		}
		public void Draw(SpriteBatch spriteBatch)
		{
			//spriteBatch.Draw(Texture, Location, Color.White);
			for(int i = 0; i < TheTrash.Count; i++)
			{
				spriteBatch.Draw(TheTrash[i].Texture, TheTrash[i].BadLocation, Color.White);
			}

		}

		public void MakeTrash(List<Texture2D> theTextures)
		{

			int howMany = rand.Next(20, 100);
			for (int i = 0; i < howMany; i++)
			{
				TrashSprite trash = new TrashSprite(theTextures);
				TheTrash.Add(trash);
				//_spriteBatch.Draw(trashTextures[rand.Next(0, trashTextures.Count)], new Vector2(x, y), Color.White);
			}

			Console.WriteLine("Making trash");
		}
	}
}