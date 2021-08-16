using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Misc
{
	class RectangleZone
	{
		public Vector2 StartPoint { get; set; }

		public Vector2 EndPoint { get; set; }



		public RectangleZone(Vector2 startPoint, int width, int height)
		{
			StartPoint = startPoint;
			EndPoint = new Vector2(startPoint.X + width, startPoint.Y + height);
		}

		public bool IsInsideOfZone(RectangleZone targetZone)
		{
			if((targetZone.StartPoint.X > this.StartPoint.X) && (targetZone.StartPoint.Y > this.StartPoint.Y))
			{
				if((targetZone.EndPoint.X < this.EndPoint.X) && (targetZone.StartPoint.Y < this.EndPoint.Y))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		public bool IsInsideOfZone(MouseState targetZone)
		{
			if ((targetZone.X > this.StartPoint.X) && (targetZone.Y > this.StartPoint.Y))
			{
				if ((targetZone.X < this.EndPoint.X) && (targetZone.Y < this.EndPoint.Y))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

	}
}
