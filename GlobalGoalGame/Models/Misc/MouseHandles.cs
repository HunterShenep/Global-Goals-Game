using GlobalGoalGame.Models;
using GlobalGoalGame.Models.Button;
using GlobalGoalGame.Models.Misc;
using GlobalGoalGame.Models.Placeable;
using GlobalGoalGame.Models.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	class MouseHandles
	{
		bool mouseReleased = true;
		float mouseTargetDist;
		float mouseTargetDistInfoBox;

		public void Update(GameTime gameTime, MouseState theState)
		{
			

			//MOUSE CLICK logic handling
			if (theState.LeftButton == ButtonState.Pressed && mouseReleased == true)
			{
				//COVERS OAKTREEBUTTON/SOLAR/ETC
				foreach (SpriteButton sb in SpriteButton.Buttons)
				{
					mouseTargetDist = Vector2.Distance(new Vector2(theState.X, theState.Y), sb.Location);
					if(mouseTargetDist < 35)
					{
						sb.DoStuff(gameTime, theState);
					}
				}

				//TrashSprite HelpBoxes
				foreach(TrashSprite t in TrashSprite.TheTrash)
				{
					if (!t.HelpBox.Visible)
					{
						if (t.HelpBox.SpriteZone.IsInsideOfZone(theState))
						{
							t.HelpBox.Visible = true;
						}
					}
					else
					{
						if (t.HelpBox.OKButton.IsInsideOfZone(theState))
						{
							t.HelpBox.Visible = false;
						}
					}
				}

				//OakTree HelpBoxes
				foreach (OakTree t in OakTree.TheOakTrees)
				{
					if (!t.HelpBox.Visible)
					{
						if (t.HelpBox.SpriteZone.IsInsideOfZone(theState))
						{
							t.HelpBox.Visible = true;
						}
					}
					else
					{
						if (t.HelpBox.OKButton.IsInsideOfZone(theState))
						{
							t.HelpBox.Visible = false;
						}
					}
				}

				//SolarPanel HelpBoxes
				foreach (SolarPanel t in SolarPanel.TheSolarPanels)
				{
					Console.WriteLine("a");
					if (!t.Button && !t.Draggable)
					{
						if (!t.HelpBox.Visible)
						{
							if (t.HelpBox.SpriteZone.IsInsideOfZone(theState))
							{
								t.HelpBox.Visible = true;
							}
						}
						else
						{
							if (t.HelpBox.OKButton.IsInsideOfZone(theState))
							{
								t.HelpBox.Visible = false;
							}
						}
					}

				}

				//Wind Turbine HelpBoxes
				foreach (WindTurbine t in WindTurbine.TheWindTurbines)
				{
					Console.WriteLine("a");
					if (!t.Button && !t.Draggable)
					{
						if (!t.HelpBox.Visible)
						{
							if (t.HelpBox.SpriteZone.IsInsideOfZone(theState))
							{
								t.HelpBox.Visible = true;
							}
						}
						else
						{
							if (t.HelpBox.OKButton.IsInsideOfZone(theState))
							{
								t.HelpBox.Visible = false;
							}
						}
					}

				}

				//Beggining helpbox
				foreach (HelpBox hb in HelpBox.TheHelpBoxes)
				{
					//If the mouse is inside of the OK button's zone.
					if (hb.Visible)
					{
						if (hb.OKButton.IsInsideOfZone(theState))
						{

							if (hb.CheckForFirstTime(hb))
							{
								break;
							}
							else
							{

								hb.Visible = false;
							}


						}
					}

				}

				mouseReleased = false;
			}

			if (theState.LeftButton == ButtonState.Released)
			{
				mouseReleased = true;
			}

			//END #BUILD MENU BUTTON HANDLING LOGIC

			//Mouse hovering infoBox's #######
			if (Game1.OneSecPassed)
			{
				//Trash Sprites
				foreach(TrashSprite t in TrashSprite.TheTrash)
				{
					mouseTargetDistInfoBox = Vector2.Distance(new Vector2(theState.X, theState.Y), t.Location);
					if(mouseTargetDistInfoBox < 25)
					{
						//Vector2 newLocation = new Vector2(t.Location.X - 150, t.Location.Y);
						InfoBox.Create(t.Name + "\n$" + t.Value.ToString("0.00"), t.Location, t.ID);
					}
				}

				//Sprite Buttons
				foreach (SpriteButton sb in SpriteButton.Buttons)
				{
					mouseTargetDistInfoBox = Vector2.Distance(new Vector2(theState.X, theState.Y), sb.Location);
					if (mouseTargetDistInfoBox < 25)
					{
						Vector2 newLocation = new Vector2(sb.Location.X - 150, sb.Location.Y);
						InfoBox.Create(sb.Name + " $" + sb.Cost.ToString("0.00"), newLocation, sb.ID);
					}
				}

				//Placeable / Plantable / Monetizeable 
				foreach(OakTree ot in OakTree.TheOakTrees)
				{
					mouseTargetDistInfoBox = Vector2.Distance(new Vector2(theState.X, theState.Y), ot.BadLocation);
					if (mouseTargetDistInfoBox < 25)
					{
						Vector2 newLocation = new Vector2(ot.Location.X - 100, ot.Location.Y);
						InfoBox.Create("Oxygen Per Second\n             " + ot.OxygenPerSecond + " kg\n", newLocation, ot.Uuid);
					}
				}

				
			}

		}
	}
}
