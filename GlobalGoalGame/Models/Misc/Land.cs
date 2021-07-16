using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	class Land
	{
		public TrashSprite LandTrash { get; }

		//public int LandNumber;

		public Land(int numberOfLand, List<Texture2D> theTextures)
		{
			LandTrash = new TrashSprite(theTextures);
		}

	}
}
