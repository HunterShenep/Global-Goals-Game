using GlobalGoalGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;
using GlobalGoalGame.Models.Misc;

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

		public bool Fruit { get; set; }

		public int TimeSinceLastFruit { get; set; }

		public int TimeUntilNextFruit { get; set; }


		//STATIC

		public static float Cost = 10.0f;

		public static List<OakTree> TheOakTrees = new List<OakTree>();

		public static List<Texture2D> Textures = new List<Texture2D>();

		//More = longer
		private static int growthMultiplier = 1;

		public OakTree()
		{
			MoneyPerEvent = 0;
			MonetizeableTree = false;
			OxygenPerSecond = 0;
			Rand = new Random();
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

			MonetizeableTree = false;
			MoneyPerEvent = 5.00f;
			Fruit = false;
			TimeSinceLastFruit = 0;
			TimeUntilNextFruit = 0;
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
					
					foreach(OakTree ot in TheOakTrees)
					{
						if (!ot.Draggable)
						{
							Statistics.TotalOxygenProduced += ot.OxygenPerSecond;
							ot.TimeAlive++;
							TreeEvolve(ot);

							//Debug.WriteLine(ot.ToString());



							if (ot.MonetizeableTree)
							{
								if(ot.TimeSinceLastFruit == ot.TimeUntilNextFruit)
								{
									if (!ot.Fruit)
									{
										ot.Fruit = true;
										ot.Texture = Textures[5];
									}
								}
								else
								{
									ot.TimeSinceLastFruit++;
								}
							}


						}
					}

					

				}
			}
		}

		private void TreeEvolve(OakTree ot)
		{
			
			if(ot.TimeAlive == 1 * growthMultiplier)
			{
				ot.Texture = Textures[1];
				ot.OxygenPerSecond = 0.0001f;
			}
			else if(ot.TimeAlive == 2 * growthMultiplier)
			{
				ot.Texture = Textures[2];
				ot.OxygenPerSecond = 0.0002f;
			}
			else if (ot.TimeAlive == 3 * growthMultiplier)
			{
				ot.Texture = Textures[3];
				ot.OxygenPerSecond = 0.0003f;
			}
			else if (ot.TimeAlive == 4 * growthMultiplier)
			{
				ot.Texture = Textures[4];
				ot.OxygenPerSecond = 0.0005f;
			}
			else if (ot.TimeAlive == 5 * growthMultiplier)
			{
				ot.MonetizeableTree = true;
				ot.TimeUntilNextFruit = Rand.Next(1 * growthMultiplier, 3 * growthMultiplier);
			}
		}

		public override string ToString()
		{
			return ("uuid: " + Uuid + "/ TimeAlive: " + TimeAlive);
		}
	}
}
