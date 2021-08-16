﻿using GlobalGoalGame.Models.Misc;
using GlobalGoalGame.Models.Trees;
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
	class OakTreeButton : SpriteButton
	{

		bool mReleased = true;
		int clickCount = 0;

		public OakTreeButton(String name, Texture2D texture, Vector2 location, int width, int height, float cost)
			: base(name, texture, location, width, height, cost)
		{

		}

		public override void Update(GameTime gameTime, MouseState mState)
		{

			if (OakTree.TheOakTrees.Count > 0)
			{
				//UPDATING LOCATION ONLY
				foreach (OakTree ot in OakTree.TheOakTrees)
				{
					if (ot.Draggable)
					{
						ot.Texture = OakTree.Textures[2];
						ot.BadLocation = new Vector2(mState.X, mState.Y);
						ot.Location = new Vector2(ot.BadLocation.X + (OakTree.TEXTURE_WIDTH/2), ot.BadLocation.Y + (OakTree.TEXTURE_HEIGHT/2));
					}
				}


				//MOUSE LOGIC
				if (mState.LeftButton == ButtonState.Pressed && mReleased == true)
				{
					clickCount++;
					if (clickCount > 1)
					{
						foreach (OakTree s in OakTree.TheOakTrees)
						{
							if (s.Draggable)
							{
								s.Draggable = false;
								s.Texture = OakTree.Textures[0];
								clickCount = 0;
								MediaPlayer.Play(Game1.OtherNoise);
								s.HelpBox.SpriteZone = new RectangleZone(s.BadLocation, OakTree.TEXTURE_WIDTH, OakTree.TEXTURE_HEIGHT);
							}
							else
							{
								clickCount = 0;
							}
						}
					}
					mReleased = false;

				}

				if (mState.LeftButton == ButtonState.Released)
				{
					mReleased = true;
				}


				//base.Update(gameTime, mState);
			}
		}

		public override void DoStuff(GameTime gameTime, MouseState mState)
		{
			if(Statistics.Money >= OakTree.Cost)
			{
				Statistics.Money -= OakTree.Cost;
				OakTree dragOakTree = new OakTree(OakTree.Textures[0], new Vector2(mState.X, mState.Y), true);
				OakTree.TheOakTrees.Add(dragOakTree);
			}
		}

	}
}
