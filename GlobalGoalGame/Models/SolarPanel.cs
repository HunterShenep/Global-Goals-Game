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
		public float Cost { get; set; }

		public Texture2D Texture { get; }

		public static List<Texture2D> Textures { get; set; }

		//The living solar panels
		public static List<SolarPanel> TheSolarPanels = new List<SolarPanel>();

		public Random Rand { get; set; }

		public SolarPanel()
		{

		}

		public SolarPanel(Texture2D texture)
		{
			Rand = new Random();
			Uuid = Rand.Next(1, 50000);
			MoneyPerHour = 1;
			Cost = 50;
			Texture = texture;
		}






		public void LoadContent() {
			
		}

		public void Update(GameTime gameTime)
		{
			//throw new NotImplementedException();
		}
		public void Draw(SpriteBatch spriteBatch)
		{
			//throw new NotImplementedException();
		}


	}
}
