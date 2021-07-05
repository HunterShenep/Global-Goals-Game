using GlobalGoalGame.Models;
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
		Texture2D background_sprite;
		Texture2D topLeftPanel;


		public List<Texture2D> TrashTextures = new List<Texture2D>();
		Random rand = new Random();

		//SpriteFont mainFont;
		SpriteFont statsFont;

		public static int GameWidth = 1400;
		public static int GameHeight = 800;

		public static float Money = 50;

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

			
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);
			background_sprite = Content.Load<Texture2D>("grass_bg");
			topLeftPanel = Content.Load<Texture2D>("top-left-for-money");
			statsFont = Content.Load<SpriteFont>("statsFont");

			//SOLAR PANEL TEXTURE #####

			SolarPanel.Textures.Add(Content.Load<Texture2D>("solar-panel"));
			solarPanel = new SolarPanel();

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

			man.Update(gameTime);
			trash.Update(gameTime);

			foreach(SolarPanel s in SolarPanel.TheSolarPanels)
			{
				s.Update(gameTime);
			}

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			_spriteBatch.Begin();

			_spriteBatch.Draw(background_sprite, new Vector2(0, 0), Color.White);

			_spriteBatch.Draw(topLeftPanel, new Vector2(5, 5), Color.White);

			_spriteBatch.DrawString(statsFont, ("Money: $" + Money.ToString("0.00")), new Vector2(15, 15), Color.White);
			man.Draw(_spriteBatch);
			trash.Draw(_spriteBatch);
			_spriteBatch.End();

			base.Draw(gameTime);
		}


	}
}
