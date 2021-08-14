using GlobalGoalGame.Models;
using GlobalGoalGame.Models.Button;
using GlobalGoalGame.Models.Misc;
using GlobalGoalGame.Models.Placeable;
using GlobalGoalGame.Models.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.IO;

namespace GlobalGoalGame
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;

		public GameClock GameClock;
		private ManSprite man;
		private TrashSprite trash;
		private SolarPanel solarPanel;
		private WindTurbine windTurbine;
		private OakTree oakTree;
		Texture2D background_sprite;
		Texture2D topLeftPanel;
		Texture2D componentsPanel;
		MouseHandles mHandler;
		public static String update;
		public List<Texture2D> TrashTextures = new List<Texture2D>();
		Random rand = new Random();
		private int OneSecCounter = 0;
		public static bool OneSecPassed = false;
		public static bool HalfSecondPassed = false;
		public InfoBox InfoBox;

		//STATIC STATISTICS
		public static float Money = 5000;
		public static float TotalOxygenProduced = 0f;


		//SpriteFont mainFont;
		SpriteFont statsFont;
		private SpriteFont sansation_16;
		private SpriteFont sansation_22;
		private SpriteFont sansation_25;

		private SpriteFont sansation_bold_22;
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

			statsFont = Content.Load<SpriteFont>("statsFont");


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

			//TRASH TEXTURES
			TrashTextures.Add(Content.Load<Texture2D>("bags"));
			TrashTextures.Add(Content.Load<Texture2D>("cigbuts"));
			TrashTextures.Add(Content.Load<Texture2D>("coke"));
			TrashTextures.Add(Content.Load<Texture2D>("doritos"));
			TrashTextures.Add(Content.Load<Texture2D>("pack-rings"));
			TrashTextures.Add(Content.Load<Texture2D>("sprite"));
			trash = new TrashSprite(TrashTextures);
			trash.MakeTrash(TrashTextures);


			InfoBox = new InfoBox();
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			GameClock.Tick();

			myCounterStuff();
			man.Update(gameTime);
			trash.Update(gameTime);
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

			_spriteBatch.Draw(topLeftPanel, new Vector2(5, 5), Color.White);

			_spriteBatch.Draw(componentsPanel, new Vector2(1230, 10), Color.White);

			foreach(SpriteButton s in SpriteButton.Buttons)
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


			
			_spriteBatch.DrawString(sansation_22, ("Money: $" + Money.ToString("0.00")), new Vector2(15, 15), Color.White);
			_spriteBatch.DrawString(sansation_bold_25, GameClock.GetTimeOfDayString() , new Vector2(645, 15), Color.Black);
			man.Draw(_spriteBatch);
			trash.Draw(_spriteBatch);

			foreach (InfoBox t in InfoBox.TheInfoBoxes)
			{
				_spriteBatch.DrawString(sansation_bold_22, t.Message, t.Location, Color.Black);
			}

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


	}
}
