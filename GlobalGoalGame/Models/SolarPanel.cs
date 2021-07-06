using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models
{
	class SolarPanel : Monetizable
	{
		public int Uuid { get; set; }
		public float MoneyPerHour { get; set; }
		public Texture2D Texture { get; }
		
		public bool Draggable { get; set; }
		public Vector2 Location { get; set; }
		public Vector2 BadLocation { get; set; }
	

		//STATIC
		public static float Cost { get; set; }
		public static List<Texture2D> Textures { get; set; }
		public static List<SolarPanel> TheSolarPanels = new List<SolarPanel>();
		//END STATIC

		public Random Rand { get; set; }

		public SolarPanel()
		{
			Textures = new List<Texture2D>();
			Cost = 50;
		}

		public SolarPanel(Texture2D texture, Vector2 location, bool draggable)
		{
			Rand = new Random();
			Uuid = Rand.Next(1, 50000);
			MoneyPerHour = 1;
			Texture = texture;
			BadLocation = location;
			Location = new Vector2(location.X - 30, location.Y - 25);
			Draggable = draggable;
		}






		public void LoadContent() {
			
		}

		public void Update(GameTime gameTime)
		{
			if(TheSolarPanels.Count > 0)
			{
				foreach(SolarPanel s in TheSolarPanels)
				{
					if (!s.Draggable)
					{
						if ((int)gameTime.TotalGameTime.Ticks % 60 == 0)
						{
							Game1.Money += 0.01f;
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
