using GlobalGoalGame.Models.Misc;
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
		public string Name { get; }
		public Vector2 BadLocation { get; }
		public Vector2 Location { get; }
		public float Value { get; }
		public int ID { get; set; }
		
		public HelpBox HelpBox { get; set; }

		public bool Exists { get; set; }

		//STATIC
		private static int TimedTrashTimer = 0;
		private static int TimedTrashTimerMax = 5;


		//private SpriteBatch _spriteBatch;
		
		// The trash 
		public static List<TrashSprite> TheTrash = new List<TrashSprite>();

		Random rand = new Random();

		public TrashSprite(List<Texture2D> theTextures)
		{
			int textureIndex = rand.Next(0, theTextures.Count);
			Texture = theTextures[textureIndex];
			if(textureIndex == 0)
			{
				Name = "Pile of Bags";
			}
			else if(textureIndex == 1)
			{
				Name = "Cig Butts";
			}
			else if (textureIndex == 2)
			{
				Name = "Soda Can";
			}
			else if (textureIndex == 3)
			{
				Name = "Chip Bag";
			}
			else if (textureIndex == 4)
			{
				Name = "Plastic Rings";
			}
			else if (textureIndex == 5)
			{
				Name = "Soda Bottle";
			}

			Value = (float)(rand.Next(1, 300) * 0.01);

			int x = rand.Next(190, (Game1.GameWidth - 200));
			int y = rand.Next(50, (Game1.GameHeight - 50));

			ID = rand.Next(1, 60000) + rand.Next(1, 23452);

			BadLocation = new Vector2(x, y);
			Location = new Vector2(BadLocation.X + 15, BadLocation.Y + 15);


			RectangleZone zone = new RectangleZone(BadLocation, 50, 50);

			HelpBox = new HelpBox(zone, Name, 1, false, "This is a piece of garbage you found on your land. It is worth $" + Value.ToString("0.00") + " if " +
				"cleaned up. ");

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

		public void MakeTrash(List<Texture2D> theTextures, int min, int max)
		{


			int howMany = rand.Next(min, max);
			for (int i = 0; i < howMany; i++)
			{
				TrashSprite trash = new TrashSprite(theTextures);

				TheTrash.Add(trash);

			}


				Console.WriteLine("Making trash");
		}

		public void TimedTrashIncrease()
		{
			if (Game1.OneSecPassed)
			{
				TimedTrashTimer++;

				if(TimedTrashTimer == TimedTrashTimerMax)
				{
					TimedTrashTimer = 0;
					Random rand = new Random();
					TimedTrashTimerMax = rand.Next(3, 30);

					MakeTrash(Game1.TrashTextures, 0, 3);
				}
			}
		}
	}
}