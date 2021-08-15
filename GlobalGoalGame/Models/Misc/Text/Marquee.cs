using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Misc.Text
{
	class Marquee
	{
		public string Text { get; set; }

		public int Iteration { get; set; }

		public Vector2 Location { get; set; }
		public Color TextColor { get; set; }
		public Color StrokeColor { get; set; }

		public const int MAX_ITERATION = 500;

		public static List<Marquee> TheMarquees = new List<Marquee>();

		public Marquee(string text, Vector2 location, Color textColor, Color strokeColor)
		{
			Text = text;
			Iteration = 0;
			Location = location;
			TextColor = textColor;
			StrokeColor = strokeColor;
		}

		public static Marquee CreateMarquee(Vector2 location, string text, Color textColor, Color strokeColor)
		{
			Marquee m = new Marquee(text, location, textColor, strokeColor);

			TheMarquees.Add(m);

			return m;
		}

		public static void Tick()
		{
			foreach(Marquee m in TheMarquees)
			{
				if(m.Iteration < MAX_ITERATION)
				{
					m.Iteration++;
					Vector2 newLocation = new Vector2(m.Location.X, m.Location.Y + 2);
					m.Location = newLocation;
				}
				else
				{
					TheMarquees.Remove(m);
					break;
				}
			}
		}

	}
}
