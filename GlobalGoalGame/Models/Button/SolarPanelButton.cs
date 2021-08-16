using GlobalGoalGame.Models.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GlobalGoalGame.Models.Button
{
	class SolarPanelButton : SpriteButton
	{

		bool mReleased = true;
		int clickCount = 0;

		public SolarPanelButton(String name, Texture2D texture, Vector2 location, int width, int height, float cost) 
			: base(name, texture, location, width, height, cost)
		{
			
		}

		public override void Update(GameTime gameTime, MouseState mState)
		{
			//Debug.WriteLine("Solar List Size:  " + SolarPanel.TheSolarPanels.Count);

			if (SolarPanel.TheSolarPanels.Count > 0)
			{
				//UPDATING LOCATION ONLY
				foreach (SolarPanel s in SolarPanel.TheSolarPanels)
				{
					if (s.Draggable)
					{
						
						s.BadLocation = new Vector2(mState.X, mState.Y);
						s.Location = new Vector2(s.BadLocation.X + (SolarPanel.TEXTURE_WIDTH/2), s.BadLocation.Y + (SolarPanel.TEXTURE_HEIGHT/2));

					}
				}


				//MOUSE LOGIC
				if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
				{
					clickCount ++;

					foreach (SolarPanel s in SolarPanel.TheSolarPanels)
					{
						if (s.Draggable && clickCount > 1)
						{
							s.Draggable = false;
							clickCount = 0;
							MediaPlayer.Play(Game1.OtherNoise);
							s.HelpBox.SpriteZone = new RectangleZone(s.BadLocation, SolarPanel.TEXTURE_WIDTH, SolarPanel.TEXTURE_HEIGHT);
						}
					}
					mReleased = false;
				}

				if (mState.LeftButton == ButtonState.Released)
				{
					mReleased = true;
				}

			}
		}

		
		public override void DoStuff(GameTime gameTime, MouseState mState)
		{

			if(Statistics.Money >= SolarPanel.Cost)
			{

				Statistics.Money -= SolarPanel.Cost;
				SolarPanel dragSolarPanel = new SolarPanel(SolarPanel.Textures[0], new Vector2(mState.X, mState.Y), true);
				SolarPanel.TheSolarPanels.Add(dragSolarPanel);
				Debug.WriteLine("SOLAR DoStuff()");

			}
		}


	}
}
