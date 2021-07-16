using GlobalGoalGame.Models.Button;
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

		public void Update(GameTime gameTime, MouseState theState)
		{
			

			//BUILD MENU BUTTON HANDLING LOGIC
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

				mouseReleased = false;
			}

			if (theState.LeftButton == ButtonState.Released)
			{
				mouseReleased = true;
			}

			//END #BUILD MENU BUTTON HANDLING LOGIC

		}
	}
}
