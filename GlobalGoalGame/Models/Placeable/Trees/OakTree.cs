using GlobalGoalGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace GlobalGoalGame.Models.Trees
{
	class OakTree : Plantable
	{
		public const int TEXTURE_WIDTH = 75;
		public const int TEXTURE_HEIGHT = 105;

		public int Uuid { get; set; }
		public float OxygenPerSecond { get; set; }
		public Vector2 BadLocation { get; set; }
		public Vector2 Location { get; set; }
		public bool Draggable { get; set; }
		public bool MonetizeableTree { get; set; }
		public float MoneyPerEvent { get; set; }
		public Random Rand { get; set; }

		public int TimeAlive { get; set; }

		public Texture2D Texture { set; get; }

		public static float Cost = 0.50f;

		public static List<OakTree> TheOakTrees = new List<OakTree>();

		public static List<Texture2D> Textures = new List<Texture2D>();

		public OakTree()
		{
			MoneyPerEvent = 0;
			MonetizeableTree = false;
			OxygenPerSecond = 0;
		}

		public OakTree(Texture2D texture, Vector2 location, bool draggable)
		{
			Rand = new Random();
			Uuid = Rand.Next(1, 50000);
			Texture = texture;
			BadLocation = location;
			Location = new Vector2(location.X + (TEXTURE_WIDTH/2), location.Y + (TEXTURE_HEIGHT/2));
			Draggable = draggable;
			TimeAlive = 0;
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
			if (Game1.OneSecPassed)
			{
				if(TheOakTrees.Count > 0)
				{
					//Add oxygen to total count
					foreach(OakTree ot in TheOakTrees)
					{
						if (!ot.Draggable)
						{
							Game1.TotalOxygenProduced += OxygenPerSecond;
							ot.TimeAlive++;
							TreeEvolve(ot);
							Debug.WriteLine(ot.ToString());
						}
					}

					

				}
			}
		}

		private void TreeEvolve(OakTree ot)
		{
			if(ot.TimeAlive == 10)
			{
				ot.Texture = Textures[1];
				ot.OxygenPerSecond = 0.0001f;
			}
			else if(ot.TimeAlive == 20)
			{
				ot.Texture = Textures[2];
				ot.OxygenPerSecond = 0.0002f;
			}
			else if (ot.TimeAlive == 30)
			{
				ot.Texture = Textures[3];
				ot.OxygenPerSecond = 0.0003f;
			}
			else if (ot.TimeAlive == 40)
			{
				ot.Texture = Textures[4];
				ot.OxygenPerSecond = 0.0005f;
			}
		}

		public override string ToString()
		{
			return ("uuid: " + Uuid + "/ TimeAlive: " + TimeAlive);
		}
	}
}
