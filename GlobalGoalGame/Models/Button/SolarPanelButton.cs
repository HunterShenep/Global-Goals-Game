﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

		public SolarPanelButton(String name, Texture2D texture, Vector2 location, int width, int height) 
			: base(name, texture, location, width, height)
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

			if(Game1.Money >= SolarPanel.Cost)
			{

				Game1.Money -= SolarPanel.Cost;
				SolarPanel dragSolarPanel = new SolarPanel(SolarPanel.Textures[0], new Vector2(mState.X, mState.Y), true);
				SolarPanel.TheSolarPanels.Add(dragSolarPanel);
				Debug.WriteLine("SOLAR DoStuff()");

			}
		}


	}
}
