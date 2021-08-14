using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Misc
{

	public class InfoBox
	{
		public static List<InfoBox> TheInfoBoxes = new List<InfoBox>();

		public string Message { get; set; }

		private int timeAlive { get; set; }

		private bool active { get; set; }

		public InfoBox()
		{

		}

		private InfoBox(string msg)
		{
			Message = msg;
			active = true;
			timeAlive = 0;
		}

		public static void Create(string msg)
		{
			TheInfoBoxes.Add(new InfoBox(msg));
		}

		private void removeInactive()
		{
			for(int i = 0; i < TheInfoBoxes.Count; i++)
			{
				if(TheInfoBoxes[i].active == false)
				{
					//This may cause issue?
					TheInfoBoxes.RemoveAt(i);
				}
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach(InfoBox i in TheInfoBoxes)
			{
				if (Game1.OneSecPassed)
				{
					timeAlive++;
					if(timeAlive == 5)
					{
						active = false;
					}
				}
				
			}
		}

	}
}
