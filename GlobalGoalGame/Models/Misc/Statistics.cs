using GlobalGoalGame.Models.Trees;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Misc
{
	public class Statistics
	{
		//STATIC STATISTICS
		public static float Money = 50;

		
		public static float TotalOxygenProduced = 0f;
		public static float OxygenPerYear = 0f;

		public static float TotalKilowattsProducted = 0f;
		public static float KilowattsPerYear = 0f;

		public static void CalculateStatistics()
		{
			if (Game1.OneSecPassed)
			{
				float OxygenPerMinute = 0f;
				foreach (OakTree ot in OakTree.TheOakTrees)
				{
					//One is in game time, one is in real time, do not be confused by names Minute / Second. (laziness)
					OxygenPerMinute += ot.OxygenPerSecond;
				}

				OxygenPerYear = (((OxygenPerMinute * 60) * 24) * 365);

				float KilowattsPerMinute = 0f;

				foreach(SolarPanel s in SolarPanel.TheSolarPanels)
				{
					KilowattsPerMinute += s.KWPerMinute;
				}

				KilowattsPerYear = (((KilowattsPerMinute * 60) * 24) * 365);
			}


		}
	}

}
