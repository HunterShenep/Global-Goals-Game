using GlobalGoalGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace GlobalGoalGame.Models.Trees
{
	class OakTree : Plantable
	{
		public int Uuid { get; set; }
		public float OxygenPerSecond { get; set; }
		public Vector2 BadLocation { get; set; }
		public Vector2 Location { get; set; }
		public bool Draggable { get; set; }
		public bool MonetizeableTree { get; set; }
		public float MoneyPerEvent { get; set; }
		public Random Rand { get; set; }

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


		public void Draw(SpriteBatch spriteBatch)
		{
			throw new NotImplementedException();
		}

		public void LoadContent()
		{

		}

		public void Update(GameTime gameTime)
		{
			throw new NotImplementedException();
		}
	}
}
