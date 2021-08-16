using GlobalGoalGame.Models.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GlobalGoalGame.Models.Misc
{
	class HelpBox
	{
		public string Name { get; }
		public RectangleZone OKButton { get; }

		public RectangleZone SpriteZone { get; set; }

		public Vector2 Location { get; }

		public string Message { get; }

		public List<string> MessageLines { get; set; }

		public int Size { get; }

		public bool Visible { get; set; }



		//Static
		public static List<Texture2D> Textures = new List<Texture2D>();
		public static List<HelpBox> TheHelpBoxes  = new List<HelpBox>();

		//const
		private const int SIZE_1_MAX_LINE_CHAR = 50;

		//FOR the begginning hel boxes
		public HelpBox(string name, int size, bool visible, string message)
		{
			Name = name;
			//Zone = zone;
			Size = size;
			Visible = visible;
			Message = message;
			if(Size == 1)
			{
				Location = new Vector2(460, 150);

				OKButton = new RectangleZone(new Vector2(900, 500), 50, 30);

			}
			
			MessageLines = splitText(Message);
		}

		//For everything else
		public HelpBox(RectangleZone zone, string name, int size, bool visible, string message)
		{
			Name = name;
			//Zone = zone;
			Size = size;
			Visible = visible;
			Message = message;
			if (Size == 1)
			{
				Location = new Vector2(460, 150);

				OKButton = new RectangleZone(new Vector2(900, 500), 50, 30);

			}
			SpriteZone = zone;


			MessageLines = splitText(Message);
		}


		private List<string> splitText(string message)
		{
			List<string> lines = new List<string>();

			int iterations = message.Length / SIZE_1_MAX_LINE_CHAR;

			if(message.Length % SIZE_1_MAX_LINE_CHAR > 0)
			{
				iterations++;
			}

			for(int i = 1; i <= iterations; i++)
			{
				if(i == 1)
				{
					if (message.Length < SIZE_1_MAX_LINE_CHAR)
					{
						lines.Add(message.Substring(0, message.Length));
					}
					else
					{
						lines.Add(message.Substring(0, SIZE_1_MAX_LINE_CHAR));
					}
				}
				else if (i == iterations)
				{
					lines.Add(message.Substring((i-1) * SIZE_1_MAX_LINE_CHAR, message.Length % SIZE_1_MAX_LINE_CHAR));
				}
				else
				{
					int a = (i-1) * SIZE_1_MAX_LINE_CHAR;
					int b = SIZE_1_MAX_LINE_CHAR;

					lines.Add(message.Substring(a , b));
				}
				
			}
			
			foreach(string s in lines)
			{
				s.Trim();
			}
			return lines;
		}

		public static void PopulateHelpBoxes()
		{
			TheHelpBoxes.Add(new HelpBox("Welcome", 1, true, "Welcome to the Global Goals Game. This simulation was created while attending " +
				" Western Technical College's game development course. " +
				"This game is focused on creating awareness of climate action(#13), affordable and clean energy (#7), and economic growth (#8). " +
				" The point of this game is to generate enough money to purchase eco friendly ways to make more residual income, and provide clean " +
				"air, energy and food while keeping your land free of trash."));

			TheHelpBoxes.Add(new HelpBox("Controls", 1, false, "To play, use keys (W,A,S,D) to move. Use key (Spacebar) to " +
				"pick up trash and interact with your placeable objects. Hovering your mouse over objects will yield basic data. " +
				" Clicking on objects will provide more information."));
			TheHelpBoxes.Add(new HelpBox("Money", 1, false, "Money is used for purchasing tree plants, solar panels, and wind turbines. You " +
				"receive money when trash is picked up, trees' fruit is collected, and automatically from Solar Panels / Wind Turbines."));
			TheHelpBoxes.Add(new HelpBox("Placeable Objects", 1, false, "Towards the top right of the screen, you should see a panel with placeable " +
				"objects. Hover your mouse over each object to see its price. Click on the object and then where you would like to place it."));
			TheHelpBoxes.Add(new HelpBox("Credits", 1, false, "Credits to myself, Hunter Shenep. Some graphics used in this simulation were " +
				" provided free of charge at https://itch.io/.\n\n\nHunterShenep.com"));
		}


		public static void Update(SpriteBatch _spriteBatch, SpriteFont font)
		{
			drawHelpBoxes(_spriteBatch, TheHelpBoxes, font);
			
			foreach(TrashSprite t in TrashSprite.TheTrash)
			{
				if (t.HelpBox.Visible)
				{
					drawHelpBox(_spriteBatch, t.HelpBox, font);
				}
			}

			foreach(OakTree ot in OakTree.TheOakTrees)
			{
				if (ot.HelpBox.Visible)
				{
					drawHelpBox(_spriteBatch, ot.HelpBox, font);
				}
			}

			foreach (SolarPanel ot in SolarPanel.TheSolarPanels)
			{
				if (ot.HelpBox.Visible)
				{
					drawHelpBox(_spriteBatch, ot.HelpBox, font);
				}
			}
		}


		private static void drawHelpBox(SpriteBatch _spriteBatch, HelpBox hb, SpriteFont font)
		{

				if (hb.Visible)
				{
					//Draw panel
					_spriteBatch.Draw(HelpBox.Textures[hb.Size - 1], hb.Location, Color.White);
					_spriteBatch.DrawString(font, hb.Name, new Vector2(670, 170), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

					int lineHeight = 30;

					//Write lines of text in the panel
					for (int i = 0; i < hb.MessageLines.Count; i++)
					{
						Vector2 lineLocation = new Vector2();
						if (i == 0)
						{
							lineLocation = new Vector2(hb.Location.X + 30, hb.Location.Y + 50 + (lineHeight * i));
						}
						else
						{
							lineLocation = new Vector2(hb.Location.X + 30, hb.Location.Y + (lineHeight * i) + 50);
						}
						_spriteBatch.DrawString(font, hb.MessageLines[i], lineLocation, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

					}

					_spriteBatch.DrawString(font, "OK", hb.OKButton.StartPoint, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
				}

			
		}


		private static void drawHelpBoxes(SpriteBatch _spriteBatch, List<HelpBox> boxes, SpriteFont font)
		{
			foreach (HelpBox hb in boxes)
			{
				if (hb.Visible)
				{
					//Draw panel
					_spriteBatch.Draw(HelpBox.Textures[hb.Size - 1], hb.Location, Color.White);
					_spriteBatch.DrawString(font, hb.Name, new Vector2(670, 170), Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

					int lineHeight = 30;

					//Write lines of text in the panel
					for (int i = 0; i < hb.MessageLines.Count; i++)
					{
						Vector2 lineLocation = new Vector2();

						if (i == 0)
						{
							lineLocation = new Vector2(hb.Location.X + 30, hb.Location.Y + 50 + (lineHeight * i));
						}
						else
						{
							lineLocation = new Vector2(hb.Location.X + 30, hb.Location.Y + (lineHeight * i) + 50);
						}



						_spriteBatch.DrawString(font, hb.MessageLines[i], lineLocation, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);

					}

					_spriteBatch.DrawString(font, "OK", hb.OKButton.StartPoint, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
				}

			}
		}

		public bool CheckForFirstTime(HelpBox hb)
		{
			if (hb.Name.Equals("Welcome"))
			{
				hb.Visible = false;
				IEnumerable<HelpBox> helpboxes = TheHelpBoxes;

				List<HelpBox> temp = helpboxes.Where(x => x.Name == "Controls").ToList();
				temp[0].Visible = true;
				return true;

			}
			else if (hb.Name.Equals("Controls"))
			{
				hb.Visible = false;
				IEnumerable<HelpBox> helpboxes = TheHelpBoxes;

				List<HelpBox> temp = helpboxes.Where(x => x.Name == "Money").ToList();
				temp[0].Visible = true;
				return true;
			}
			else if (hb.Name.Equals("Money"))
			{
				hb.Visible = false;
				IEnumerable<HelpBox> helpboxes = TheHelpBoxes;

				List<HelpBox> temp = helpboxes.Where(x => x.Name == "Placeable Objects").ToList();
				temp[0].Visible = true;
				return true;
			}
			else if (hb.Name.Equals("Placeable Objects"))
			{
				hb.Visible = false;
				IEnumerable<HelpBox> helpboxes = TheHelpBoxes;

				List<HelpBox> temp = helpboxes.Where(x => x.Name == "Credits").ToList();
				temp[0].Visible = true;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
