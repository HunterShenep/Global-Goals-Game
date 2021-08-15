using GlobalGoalGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	interface Energizable : Placeable
	{
		public float MoneyPerMinute { get; set; }

		public float KWPerMinute { get; set; }


	}
}
