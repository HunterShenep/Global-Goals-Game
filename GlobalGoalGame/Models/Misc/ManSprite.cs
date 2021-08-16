using GlobalGoalGame.Models.Misc;
using GlobalGoalGame.Models.Misc.Text;
using GlobalGoalGame.Models.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalGoalGame
{
	class ManSprite
	{
	
		private List<Texture2D> textures { get; set; }
		public Texture2D Texture { get; set; }
		public Vector2 BadLocation;
		public Vector2 Location;
		


		private int walkTimer = 0;

		private bool spaceReleased = true;
		public ManSprite(List<Texture2D> theTextures)
		{
			textures = theTextures;
			Texture = theTextures[0];
			BadLocation = new Vector2(400, 400);
			Location = new Vector2(this.BadLocation.X + 15, BadLocation.Y + 25);
		}

		private void Walk(bool left)
		{
			walkTimer++;

			if (walkTimer == 5)
			{
				if (left)
				{
					Texture = textures[6];
				}
				else
				{
					Texture = textures[1];
				}

			}
			else if (walkTimer == 10)
			{
				if (left)
				{
					Texture = textures[7];
				}
				else
				{
					Texture = textures[2];
				}
			}
			else if (walkTimer == 15)
			{
				if (left)
				{
					Texture = textures[8];
				}
				else
				{
					Texture = textures[3];
				}
			}
			else if (walkTimer == 20)
			{
				if (left)
				{
					Texture = textures[9];
				}
				else
				{
					Texture = textures[4];
				}
			}
			else if (walkTimer == 25)
			{
				if (left)
				{
					Texture = textures[5];
				}
				else
				{
					Texture = textures[1];
				}


				walkTimer = 0;
			}
		}

		private void DetermineWalk(KeyboardState kState)
		{
			

			if (kState.IsKeyDown(Keys.W))
			{
				if (kState.IsKeyDown(Keys.W) && kState.IsKeyDown(Keys.A))
				{
					BadLocation.Y -= 1;
					BadLocation.X -= 1;
					Walk(true);

				}
				else if (kState.IsKeyDown(Keys.W) && kState.IsKeyDown(Keys.D))
				{
					BadLocation.Y -= 1;
					BadLocation.X += 1;
					Walk(false);
				}
				else
				{
					Walk(false);
					BadLocation.Y -= 1;
				}
			}
			else if (kState.IsKeyDown(Keys.S))
			{
				if (kState.IsKeyDown(Keys.S) && kState.IsKeyDown(Keys.A))
				{
					BadLocation.Y += 1;
					BadLocation.X -= 1;
					Walk(true);
				}
				else if (kState.IsKeyDown(Keys.S) && kState.IsKeyDown(Keys.D))
				{
					BadLocation.Y += 1;
					BadLocation.X += 1;
					Walk(false);
				}
				else
				{
					BadLocation.Y += 1;
					Walk(false);
				}
			}
			else if (kState.IsKeyDown(Keys.A))
			{
				BadLocation.X -= 1;
				Walk(true);
			}
			else if (kState.IsKeyDown(Keys.D))
			{
				BadLocation.X += 1;
				Walk(false);
			}

		}

		public void Update(GameTime gameTime)
		{
			//SpriteEffects s = SpriteEffects.FlipHorizontally;



			if(Keyboard.GetState().IsKeyDown(Keys.Space) && spaceReleased == true)
			{
				float distanceFromTrash = 0f;
				

				//Check if close to trash
				for (int i = 0; i < TrashSprite.TheTrash.Count; i++)
				{
					distanceFromTrash = Vector2.Distance(TrashSprite.TheTrash[i].Location, Location);
					if(distanceFromTrash < 30f)
					{
						Statistics.Money += TrashSprite.TheTrash[i].Value;
						MediaPlayer.Play(Game1.ChaChing);
						Marquee.CreateMarquee(TrashSprite.TheTrash[i].Location, "+$" + TrashSprite.TheTrash[i].Value.ToString("0.00"), Color.White, Color.Black);
						//TrashSprite.TheTrash[i].Exists = false;
						TrashSprite.TheTrash.RemoveAt(i);

					}
				}
				float distanceFromTree = 0f;

				//Check if close to trees
				foreach(OakTree ot in OakTree.TheOakTrees)
				{
					distanceFromTree = Vector2.Distance(ot.Location, Location);
					if(distanceFromTree < 40f)
					{
						if (ot.Fruit)
						{
							MediaPlayer.Play(Game1.ChaChing);
							Statistics.Money += ot.MoneyPerEvent;
							ot.Fruit = false;
							ot.TimeSinceLastFruit = 0;
							ot.Texture = OakTree.Textures[4];
							Marquee.CreateMarquee(ot.Location, "+$" + ot.MoneyPerEvent.ToString("0.00"), Color.Gold, Color.Black);
						}

					}
				}

				spaceReleased = false;
			}
			if (Keyboard.GetState().IsKeyUp(Keys.Space))
			{
				spaceReleased = true;
			}

			DetermineWalk(Keyboard.GetState());

			Location.X = BadLocation.X + 15;
			Location.Y = BadLocation.Y + 25;

		}


		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Draw(Texture, BadLocation, Color.White);

		}

	}
}
