using GlobalGoalGame.Models;
using GlobalGoalGame.Models.Button;
using GlobalGoalGame.Models.Placeable;
using GlobalGoalGame.Models.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GlobalGoalGame
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
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


		//STATIC STATISTICS
		public static float Money = 5000;
		public static float TotalOxygenProduced = 0f;


		//SpriteFont mainFont;
		SpriteFont statsFont;

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
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			//STANDALONEs

			background_sprite = Content.Load<Texture2D>("grass_bg");
			
			topLeftPanel = Content.Load<Texture2D>("panel-for-live-stats");
			componentsPanel = Content.Load<Texture2D>("panel-for-components");

			statsFont = Content.Load<SpriteFont>("statsFont");

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
			SpriteButton.Buttons.Add(new SolarPanelButton("Solar Panel Button", SolarPanel.Textures[0], new Vector2(1240, 25), SolarPanel.TEXTURE_WIDTH, SolarPanel.TEXTURE_HEIGHT));
			SpriteButton.Buttons.Add(new OakTreeButton("Oak Tree Button", OakTree.Textures[3], new Vector2(1300, -20), OakTree.TEXTURE_WIDTH, OakTree.TEXTURE_HEIGHT));
			SpriteButton.Buttons.Add(new WindTurbineButton("Wind Turbine Button", WindTurbine.Textures[0], new Vector2(1300, 250), WindTurbine.TEXTURE_WIDTH, WindTurbine.TEXTURE_HEIGHT));

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
			
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();


			myCounterStuff();
			man.Update(gameTime);
			trash.Update(gameTime);

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
			
			_spriteBatch.DrawString(statsFont, ("Money: $" + Money.ToString("0.00")), new Vector2(15, 15), Color.Black);
			_spriteBatch.DrawString(statsFont, update, new Vector2(650, 15), Color.Black);
			man.Draw(_spriteBatch);
			trash.Draw(_spriteBatch);
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


	}
}
