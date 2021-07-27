using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Placeable
{
	class WindTurbine : Monetizable
	{
		public const int TEXTURE_WIDTH = 75;
		public const int TEXTURE_HEIGHT = 100;

		public float MoneyPerHour { get; set; }
		public int Uuid { get; set; }
		public Vector2 BadLocation { get; set; }
		public Vector2 Location { get; set; }
		public bool Draggable { get; set; }
		public Random Rand { get; set; }

		private int AnimationCounter;

		public Texture2D Texture { get; set; }


		//STATIC
		public static float Cost { get; set; }
		public static List<Texture2D> Textures { get; set; }

		public static List<WindTurbine> TheWindTurbines = new List<WindTurbine>();

		public WindTurbine()
		{
			Textures = new List<Texture2D>();
			Cost = 1000;
		}

		public WindTurbine(Texture2D texture, Vector2 location, bool draggable)
		{
			Rand = new Random();
			Uuid = Rand.Next(1, 50000);
			MoneyPerHour = 0.03f;
			Texture = texture;
			BadLocation = location;
			Location = new Vector2(location.X + (TEXTURE_WIDTH / 2), location.Y + (TEXTURE_HEIGHT / 2));
			Draggable = draggable;
			AnimationCounter = 0;
		}

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
			changeTexture();

			if (Game1.OneSecPassed)
			{
				
				if (TheWindTurbines.Count > 0)
				{
					foreach (WindTurbine s in TheWindTurbines)
					{
						if (!s.Draggable)
						{
							Game1.Money += s.MoneyPerHour;

						}

					}

				}
			}
		}

		private void changeTexture()
		{
			if (Game1.OneSecPassed)
			{

			}
			foreach (WindTurbine wt in TheWindTurbines)
				{
					if (!wt.Draggable)
					{
						wt.AnimationCounter++;
						if(wt.AnimationCounter == 0)
						{
							wt.Texture = Textures[0];
						}
						else if(wt.AnimationCounter == 1){
							wt.Texture = Textures[1];
						}
						else if (wt.AnimationCounter == 2)
						{
							wt.Texture = Textures[2];
						}
						else if (wt.AnimationCounter == 3)
						{
							wt.Texture = Textures[3];
						}
						else if (wt.AnimationCounter == 4)
						{
							wt.Texture = Textures[4];
						}
						else if (wt.AnimationCounter == 5)
						{
							wt.Texture = Textures[5];
							wt.AnimationCounter = 0;
						}
					}
				}
			
		}
	}
}
