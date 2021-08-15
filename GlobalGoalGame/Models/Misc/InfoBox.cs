using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GlobalGoalGame.Models.Misc
{

	public class InfoBox
	{
		public static List<InfoBox> TheInfoBoxes = new List<InfoBox>();

		public string Message { get; set; }

		private int timeAlive { get; set; }

		private bool active { get; set; }

		private int forID { get; set; }

		public Vector2 Location { get; }


		public InfoBox()
		{
			
		}

		private InfoBox(string msg, Vector2 loc, int forID)
		{
			Message = msg;
			active = true;
			timeAlive = 0;
			Location = loc;
			this.forID = forID;
		}

		public static void Create(string msg, Vector2 loc, int id)
		{
			bool exists = false;

			foreach(InfoBox i in TheInfoBoxes)
			{
				if(i.forID == id)
				{
					exists = true;
				}
			}

			if (!exists)
			{
				TheInfoBoxes.Add(new InfoBox(msg, loc, id));
			}

			
		}

		private void removeInactive()
		{
			
			for (int i = 0; i < TheInfoBoxes.Count; i++)
			{
				if(!TheInfoBoxes[i].active)
				{
					TheInfoBoxes.RemoveAt(i);
				}
			}
		}

		public void Update(GameTime gameTime)
		{
			if (Game1.OneSecPassed)
			{
				foreach (InfoBox i in TheInfoBoxes)
				{
					//Debug.WriteLine("TimeAlive: " + i.timeAlive + " - Active: " + i.active + " - ForID: " + i.forID);

					i.timeAlive++;

					if (i.timeAlive == 3)
					{
						//Debug.WriteLine("Old enough to delete");
						i.active = false;
					}
					
				}
				removeInactive();
			}
		}

	}
}
