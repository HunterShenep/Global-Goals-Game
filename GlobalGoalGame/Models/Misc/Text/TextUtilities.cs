using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;


namespace GlobalGoalGame.Models.Misc.Text
{
	class TextUtilities
	{
		//public void DrawString(SpriteFont spriteFont, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

		public static void DrawStroke(SpriteBatch sb, SpriteFont font, string message, Vector2 location, Color text, Color stroke)
		{
			sb.DrawString(font, message, new Vector2(location.X + 1, location.Y + 1), stroke);
			sb.DrawString(font, message, new Vector2(location.X - 1, location.Y - 1), stroke);
			sb.DrawString(font, message, location, text);
		}

	}


}
