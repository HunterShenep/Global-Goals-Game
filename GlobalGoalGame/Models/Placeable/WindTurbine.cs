using GlobalGoalGame.Models.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Placeable
{
	class WindTurbine : Energizable
	{
		public const int TEXTURE_WIDTH = 75;
		public const int TEXTURE_HEIGHT = 100;

		public float MoneyPerMinute { get; set; }
		public int Uuid { get; set; }
		public Vector2 BadLocation { get; set; }
		public Vector2 Location { get; set; }
		public bool Draggable { get; set; }
		public Random Rand { get; set; }
		public bool Button { get; set; }

		public float KWPerMinute { get; set; }

		private int AnimationCounter;

		public Texture2D Texture { get; set; }

		public HelpBox HelpBox { get; set; }


		//STATIC
		public static float Cost { get; set; }
		public static List<Texture2D> Textures { get; set; }

		public static List<WindTurbine> TheWindTurbines = new List<WindTurbine>();

		public WindTurbine()
		{
			Textures = new List<Texture2D>();
			Cost = 1000;
			Button = true;
		}

		public WindTurbine(Texture2D texture, Vector2 location, bool draggable)
		{
			Rand = new Random();
			Uuid = Rand.Next(1, 50000);
			MoneyPerMinute = 0.03f;
			Texture = texture;
			BadLocation = location;
			Location = new Vector2(location.X + (TEXTURE_WIDTH / 2), location.Y + (TEXTURE_HEIGHT / 2));
			Draggable = draggable;
			AnimationCounter = 0;
			KWPerMinute = 2f;
			HelpBox = new HelpBox(new RectangleZone(BadLocation, TEXTURE_WIDTH, TEXTURE_HEIGHT), "Wind Turbine", 1, false, "This is a wind" +
	" turbine. It is currently producing " + KWPerMinute + "Kw per minute and generating $" + MoneyPerMinute.ToString("0.000") + " per minute.");
			Button = false;
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
							Statistics.Money += s.MoneyPerMinute;
							Statistics.TotalKilowattsProducted += s.KWPerMinute;
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
