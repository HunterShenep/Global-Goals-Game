using GlobalGoalGame.Models.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GlobalGoalGame.Models
{
	class SolarPanel : Energizable
	{

		public const int TEXTURE_WIDTH = 59;
		public const int TEXTURE_HEIGHT = 50;

		public int Uuid { get; set; }
		public float MoneyPerMinute { get; set; }
		public Texture2D Texture { get; }
		
		public bool Draggable { get; set; }
		public bool Button { get; set; }
		public Vector2 Location { get; set; }
		public Vector2 BadLocation { get; set; }

		public float KWPerMinute { get; set; }

		public HelpBox HelpBox { get; set; }	
		//STATIC
		public static float Cost { get; set; }
		public static List<Texture2D> Textures { get; set; }

		public static List<SolarPanel> TheSolarPanels = new List<SolarPanel>();

		//END STATIC

		public Random Rand { get; set; }

		public SolarPanel()
		{
			Button = true;
			Textures = new List<Texture2D>();
			Cost = 50;
		}

		public SolarPanel(Texture2D texture, Vector2 location, bool draggable)
		{
			Button = false;
			Rand = new Random();
			Uuid = Rand.Next(1, 50000);
			MoneyPerMinute = 0.005f;
			Texture = texture;
			BadLocation = location;
			Location = new Vector2(location.X + (TEXTURE_WIDTH/2), location.Y + (TEXTURE_HEIGHT/2));
			Draggable = draggable;
			KWPerMinute = 0.0041f;

			HelpBox = new HelpBox(new RectangleZone(BadLocation, TEXTURE_WIDTH, TEXTURE_HEIGHT), "Solar Panel", 1, false, "This is a solar" +
				" panel. It is currently producing " + KWPerMinute + "Kw per minute and generating $" + MoneyPerMinute.ToString("0.000") + " per minute.");

		}


		public void LoadContent() {
			
		}

		public void Update(GameTime gameTime)
		{

			if (Game1.OneSecPassed)
			{
				if (TheSolarPanels.Count > 0)
				{
					foreach (SolarPanel s in TheSolarPanels)
					{
						if (!s.Draggable)
						{
							Statistics.Money += s.MoneyPerMinute;
							Statistics.TotalKilowattsProducted += s.KWPerMinute;

							//Debug.WriteLine("Paying for: " + s.Uuid);
						}

					}

				}
			}

		}
		public void Draw(SpriteBatch spriteBatch)
		{
			//throw new NotImplementedException();
		}


	}
}
