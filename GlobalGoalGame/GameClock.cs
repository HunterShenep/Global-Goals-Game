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
		private const long TICKS_PER_DAY = 5184000;
		private const long TICKS_PER_HOUR = 216000;
		private const long TICKS_PER_MINUTE = 3600;
		private const long TICKS_PER_SECOND = 60;
		

		//Other

		public GameClock()
		{

		}

		public void Tick()
		{
			TotalTicks++;
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

		public float GetTotalDays()
		{
			return TotalTicks / TICKS_PER_HOUR;
		}

		public float GetTotalHours()
		{
			return TotalTicks / TICKS_PER_MINUTE;
		}

		public float GetTotalMinutes()
		{
			return TotalTicks / TICKS_PER_SECOND;
		}

		public float GetTotalSeconds()
		{
			return 0f;
			// Implement later. 
		}

		public string GetTimeOfDayString()
		{
			int hourOfDay = ((int)GetTotalHours() % (int)TICKS_PER_MINUTE);

			int minuteOfDay = (int)GetTotalMinutes() % (int)TICKS_PER_SECOND;
			


			//int minuteOfDay = 
			return hourOfDay.ToString("00") + ":" + minuteOfDay.ToString("00");
		}


	}
}
