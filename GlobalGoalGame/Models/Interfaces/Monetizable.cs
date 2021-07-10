using GlobalGoalGame.Models.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	interface Monetizable : Placeable
	{
		public float MoneyPerHour { get; set; }


	}
}
