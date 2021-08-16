﻿using GlobalGoalGame.Models.Misc;
using GlobalGoalGame.Models.Placeable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame.Models.Button
{
	class WindTurbineButton : SpriteButton
	{
		bool mReleased = true;
		int clickCount = 0;

		public WindTurbineButton(String name, Texture2D texture, Vector2 location, int width, int height, float cost)
			: base(name, texture, location, width, height, cost)
		{

		}

		public override void Update(GameTime gameTime, MouseState mState)
		{
			//Debug.WriteLine("Solar List Size:  " + SolarPanel.TheSolarPanels.Count);

			if (WindTurbine.TheWindTurbines.Count > 0)
			{
				//UPDATING LOCATION ONLY
				foreach (WindTurbine s in WindTurbine.TheWindTurbines)
				{
					if (s.Draggable)
					{

						s.BadLocation = new Vector2(mState.X, mState.Y);
						s.Location = new Vector2(s.BadLocation.X + (WindTurbine.TEXTURE_WIDTH / 2), s.BadLocation.Y + (WindTurbine.TEXTURE_HEIGHT / 2));
					}
				}


				//MOUSE LOGIC
				if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
				{
					clickCount++;

					foreach (WindTurbine s in WindTurbine.TheWindTurbines)
					{
						if (s.Draggable && clickCount > 1)
						{
							s.Draggable = false;
							clickCount = 0;
							MediaPlayer.Play(Game1.OtherNoise);
							s.HelpBox.SpriteZone = new RectangleZone(s.BadLocation, WindTurbine.TEXTURE_WIDTH, WindTurbine.TEXTURE_HEIGHT);
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

			if (Statistics.Money >= WindTurbine.Cost)
			{

				Statistics.Money -= WindTurbine.Cost;
				WindTurbine windTurbine = new WindTurbine(WindTurbine.Textures[0], new Vector2(mState.X, mState.Y), true);
				WindTurbine.TheWindTurbines.Add(windTurbine);
				//Debug.WriteLine("SOLAR DoStuff()");

			}
		}

	}
}
