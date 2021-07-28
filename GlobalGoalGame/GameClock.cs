using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	public class GameClock
	{
		//Total ticks since start of game. No reference
		private static float TotalTicks;

		//Constants
		private const long TICKS_PER_DAY = 216000;
		private const long TICKS_PER_HOUR = 216000;
		private const long TICKS_PER_MINUTE = 3600;
		private const long TICKS_PER_SECOND = 60;
		

		//Other

		public GameClock()
		{

		}

		//to REAL TIME conversions
		private float TotalHours()
		{
			return TotalTicks / TICKS_PER_HOUR;
		}

		private float TotalMinutes()
		{
			return TotalTicks / TICKS_PER_MINUTE;
		}


		private float TotalSeconds()
		{
			return TotalTicks / TICKS_PER_SECOND;
		}

		//to GAME TIME conversions
		public float GetTotalHours()
		{
			return 0f;
			//Implement later
		}

		public float GetTotalMinutes()
		{
			return 0f;
			//Implement later
		}
		public float GetTotalSeconds()
		{
			return 0f;
			//Implement later
		}



	}
}
