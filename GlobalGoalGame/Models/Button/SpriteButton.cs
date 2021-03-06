using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Button
{
	class SpriteButton
	{
		public String Name { get; set; }
		public Texture2D Texture { get; set; }

		public Vector2 BadLocation { get; set; }
		public Vector2 Location { get; set; }

		public float Cost { get; set; }

		public int ID { get; set; }

		public static List<SpriteButton> Buttons = new List<SpriteButton>();

		public SpriteButton(String name, Texture2D texture, Vector2 location, int width, int height, float cost)
		{
			Name = name;
			Texture = texture;
			BadLocation = location;
			Location = new Vector2(BadLocation.X + (width/2), BadLocation.Y + (height/2));
			Random rand = new Random();
			ID = rand.Next(0, 60000) + rand.Next(5, 53223);
			Cost = cost;
		}

		public virtual void Update(GameTime gameTime, MouseState mState)
		{
			
		}

		public virtual void DoStuff(GameTime gameTime, MouseState mState)
		{
			Console.Write(gameTime.TotalGameTime.ToString() + ": " + Name + " Pressed");



		}

	}
}
