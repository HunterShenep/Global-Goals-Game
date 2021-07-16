using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Interfaces
{
	interface Plantable : Placeable
	{

		public float OxygenPerSecond { get; set; }

		public bool MonetizeableTree { get; set; }

		public float MoneyPerEvent { get; set; }

		public int TimeAlive { get; set; }

		public String ToString();

	}
}
