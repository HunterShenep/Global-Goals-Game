using GlobalGoalGame.Models;
using GlobalGoalGame.Models.Button;
using GlobalGoalGame.Models.Misc;
using GlobalGoalGame.Models.Misc.Text;
using GlobalGoalGame.Models.Placeable;
using GlobalGoalGame.Models.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace GlobalGoalGame
{
	public class Game1 : Game
	{

		//Vector2 itemSize = font.MeasureString("[Menu Item]");
		//The code above I stumbled on, will come in handy.

		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public GameClock GameClock;
		private ManSprite man;
		private TrashSprite trash;
		private SolarPanel solarPanel;
		private WindTurbine windTurbine;
		private OakTree oakTree;
		public InfoBox InfoBox;

		Texture2D background_sprite;
		Texture2D topLeftPanel;
		Texture2D componentsPanel;
		MouseHandles mHandler;

		public static String update;
		public static List<Texture2D> TrashTextures = new List<Texture2D>();
		Random rand = new Random();
		private int OneSecCounter = 0;
		public static bool OneSecPassed = false;
		public static bool HalfSecondPassed = false;

		public static Song ChaChing;
		public static Song OtherNoise;




		//SpriteFont mainFont;
		SpriteFont statsFont;
		private SpriteFont sansation_16;
		private SpriteFont sansation_20;
		private SpriteFont sansation_22;
		private SpriteFont sansation_25;

		private SpriteFont sansation_bold_22;
		private SpriteFont sansation_bold_23;
		private SpriteFont sansation_bold_25;
		


		private const int FontBitmapWidth = 1024;
		private const int FontBitmapHeight = 1024;

		public static int GameWidth = 1400;
		public static int GameHeight = 800;



		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;

			_graphics.PreferredBackBufferWidth = GameWidth;
			_graphics.PreferredBackBufferHeight = GameHeight;
			
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
			update = "a";
			mHandler = new MouseHandles();
			
		}

		protected override void LoadContent()
		{
			GameClock = new GameClock();
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			//STANDALONEs


			background_sprite = Content.Load<Texture2D>("grass_bg");

			topLeftPanel = Content.Load<Texture2D>("panel-for-live-stats");
			componentsPanel = Content.Load<Texture2D>("panel-for-components");
			HelpBox.Textures.Add(Content.Load<Texture2D>("panel-for-help-box"));
			HelpBox.PopulateHelpBoxes();

			statsFont = Content.Load<SpriteFont>("statsFont");
			//Sounds
			ChaChing = Content.Load<Song>("ka-ching");
			OtherNoise = Content.Load<Song>("teleport");


			//CUSTOM FONT STUFF ############################
			createFonts();
			//END CUSTOM FONT STUFF ############################

			//SOLAR PANEL TEXTURE #####
			solarPanel = new SolarPanel();
			SolarPanel.Textures.Add(Content.Load<Texture2D>("solar-panel"));

			//Oak Tree Texture ####
			oakTree = new OakTree();
			OakTree.Textures.Add(Content.Load<Texture2D>("oak1"));
			OakTree.Textures.Add(Content.Load<Texture2D>("oak2"));
			OakTree.Textures.Add(Content.Load<Texture2D>("oak3"));
			OakTree.Textures.Add(Content.Load<Texture2D>("oak4"));
			OakTree.Textures.Add(Content.Load<Texture2D>("oak5"));
			OakTree.Textures.Add(Content.Load<Texture2D>("oak6"));

			//WIND MILL
			windTurbine = new WindTurbine();
			WindTurbine.Textures.Add(Content.Load<Texture2D>("mill1"));
			WindTurbine.Textures.Add(Content.Load<Texture2D>("mill2"));
			WindTurbine.Textures.Add(Content.Load<Texture2D>("mill3"));
			WindTurbine.Textures.Add(Content.Load<Texture2D>("mill4"));
			WindTurbine.Textures.Add(Content.Load<Texture2D>("mill5"));
			WindTurbine.Textures.Add(Content.Load<Texture2D>("mill6"));

			//BUTTONS
			SpriteButton.Buttons.Add(new SolarPanelButton("Solar Panel", SolarPanel.Textures[0], new Vector2(1240, 25), SolarPanel.TEXTURE_WIDTH, SolarPanel.TEXTURE_HEIGHT, SolarPanel.Cost));
			SpriteButton.Buttons.Add(new OakTreeButton("Oak Tree", OakTree.Textures[3], new Vector2(1300, -20), OakTree.TEXTURE_WIDTH, OakTree.TEXTURE_HEIGHT, OakTree.Cost));
			SpriteButton.Buttons.Add(new WindTurbineButton("Wind Turbine", WindTurbine.Textures[0], new Vector2(1233, 86), WindTurbine.TEXTURE_WIDTH, WindTurbine.TEXTURE_HEIGHT, WindTurbine.Cost));


			//TRASH TEXTURES
			TrashTextures.Add(Content.Load<Texture2D>("bags"));
			TrashTextures.Add(Content.Load<Texture2D>("cigbuts"));
			TrashTextures.Add(Content.Load<Texture2D>("coke"));
			TrashTextures.Add(Content.Load<Texture2D>("doritos"));
			TrashTextures.Add(Content.Load<Texture2D>("pack-rings"));
			TrashTextures.Add(Content.Load<Texture2D>("sprite"));
			trash = new TrashSprite(TrashTextures);

			trash.MakeTrash(TrashTextures, 40, 50);

			//MAN SPRITE TEXTURES #####
			List<Texture2D> manTextures = new List<Texture2D>();
			manTextures.Add(Content.Load<Texture2D>("idle"));
			manTextures.Add(Content.Load<Texture2D>("walk1"));
			manTextures.Add(Content.Load<Texture2D>("walk2"));
			manTextures.Add(Content.Load<Texture2D>("walk3"));
			manTextures.Add(Content.Load<Texture2D>("walk4"));
			manTextures.Add(Content.Load<Texture2D>("idleL"));
			manTextures.Add(Content.Load<Texture2D>("walkL1"));
			manTextures.Add(Content.Load<Texture2D>("walkL2"));
			manTextures.Add(Content.Load<Texture2D>("walkL3"));
			manTextures.Add(Content.Load<Texture2D>("walkL4"));
			man = new ManSprite(manTextures);



			InfoBox = new InfoBox();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			GameClock.Tick();
			Marquee.Tick();
			

			myCounterStuff();
			Statistics.CalculateStatistics();

			trash.Update(gameTime);
			trash.TimedTrashIncrease();

			man.Update(gameTime);
			InfoBox.Update(gameTime);


			MouseState mState = Mouse.GetState();
			mHandler.Update(gameTime, mState);

			//Sprite Buttons
			foreach(SpriteButton sb in SpriteButton.Buttons)
			{
				sb.Update(gameTime, mState);
			}

			//Placeable update
			solarPanel.Update(gameTime);
			oakTree.Update(gameTime);
			windTurbine.Update(gameTime);


			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			_spriteBatch.Draw(background_sprite, new Vector2(0, 0), Color.White);

			trash.Draw(_spriteBatch);


			_spriteBatch.Draw(topLeftPanel, new Vector2(5, 5), Color.White);

			_spriteBatch.Draw(componentsPanel, new Vector2(1230, 10), Color.White);

			
			man.Draw(_spriteBatch);


			foreach (SpriteButton s in SpriteButton.Buttons)
			{
				_spriteBatch.Draw(s.Texture, s.BadLocation, Color.White);
			}

			foreach(SolarPanel sp in SolarPanel.TheSolarPanels)
			{
					_spriteBatch.Draw(sp.Texture, sp.BadLocation, Color.White);
			}
			foreach (WindTurbine sp in WindTurbine.TheWindTurbines)
			{
				_spriteBatch.Draw(sp.Texture, sp.BadLocation, Color.White);
			}

			foreach (OakTree ot in OakTree.TheOakTrees)
			{
					_spriteBatch.Draw(ot.Texture, ot.BadLocation, Color.White);
			}


			
			_spriteBatch.DrawString(sansation_20, ("Money: $" + Statistics.Money.ToString("0.00")), new Vector2(20, 15), Color.White);
			//							30 pixel gap in between sections, 20 between lines
			_spriteBatch.DrawString(sansation_20, ("Total Oxygen: " + Statistics.TotalOxygenProduced.ToString("0.00")) + "kg", new Vector2(20, 45), Color.White);
			_spriteBatch.DrawString(sansation_20, ("Oxygen/Year: " + Statistics.OxygenPerYear.ToString("0.00")) + "kg", new Vector2(20, 65), Color.White);
			
			_spriteBatch.DrawString(sansation_20, ("Total Energy: " + Statistics.TotalKilowattsProducted.ToString("0.00")) + "kw", new Vector2(20, 95), Color.White);
			_spriteBatch.DrawString(sansation_20, ("Energy/Year: " + Statistics.KilowattsPerYear.ToString("0.00")) + "kw", new Vector2(20, 115), Color.White);

			_spriteBatch.DrawString(sansation_bold_25, GameClock.GetTimeOfDayString() , new Vector2(675, 15), Color.Black);
			

			foreach (InfoBox t in InfoBox.TheInfoBoxes)
			{
				TextUtilities.DrawStroke(_spriteBatch,sansation_bold_23, t.Message, t.Location, Color.White, Color.Black);
			}

			foreach(Marquee m in Marquee.TheMarquees)
			{
				TextUtilities.DrawStroke(_spriteBatch, sansation_bold_23, m.Text, m.Location, m.TextColor, m.StrokeColor);
			}

			HelpBox.Update(_spriteBatch, sansation_20);


			_spriteBatch.End();



			base.Draw(gameTime);
		}



		private void myCounterStuff()
		{
			OneSecCounter++;

			if (OneSecCounter == 60)
			{
				OneSecPassed = true;
			}
			else if (OneSecCounter == 61)
			{
				OneSecPassed = false;
				OneSecCounter = 1;
			}

			
		}


		private void createFonts()
		{
			TtfFontBakerResult fontBakeResult;

			//Sansation Regular 16

			using (var stream = File.OpenRead("Fonts/Sansation_Regular.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					16,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_16 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}
			// ### Sansation Regular 20
			using (var stream = File.OpenRead("Fonts/Sansation_Regular.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					20,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_20 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}
			// ### Sansation Regular 22
			using (var stream = File.OpenRead("Fonts/Sansation_Regular.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					22,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_22 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}
			// ### Sansation Regular 25
			using (var stream = File.OpenRead("Fonts/Sansation_Regular.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					25,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_25 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}

			// ### SANSATION BOLD 23
			using (var stream = File.OpenRead("Fonts/Sansation_Bold.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					23,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_bold_23 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}

			// ### SANSATION BOLD 25
			using (var stream = File.OpenRead("Fonts/Sansation_Bold.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					25,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_bold_25 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}

			// ### SANSATION BOLD 22
			using (var stream = File.OpenRead("Fonts/Sansation_Bold.ttf"))
			{
				// TODO: use this.Content to load your game content here
				fontBakeResult = TtfFontBaker.Bake(stream,
					22,
					FontBitmapWidth,
					FontBitmapHeight,
					new[]
					{
					CharacterRange.BasicLatin,
					CharacterRange.Latin1Supplement,
					CharacterRange.LatinExtendedA,
					CharacterRange.Cyrillic
					}
				);

				sansation_bold_22 = fontBakeResult.CreateSpriteFont(GraphicsDevice);
			}
		}

		public static void IsTouchingOtherSprites(Texture2D texture, Vector2 location)
		{


		}


	}
}
