using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using DrawListBuddy;
using GameTimer;
using HadoukInput;
using FontBuddyLib;
using RenderBuddy;

namespace DrawListBuddySample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class Game1 : Game
	{
		#region Members

		#region Game Stuff

		GraphicsDeviceManager graphics;

		GameClock _clock;

		InputState _inputState;

		InputWrapper _inputWrapper;
	
		FontBuddy _font;

		#endregion Game Stuff

		/// <summary>
		/// Thing for showing how the drawlist works
		/// </summary>
		Grid _Grid;

		/// <summary>
		/// the renderer we will use for darwing
		/// </summary>
		XNARenderer _Renderer;

		//the drawlist we are gonn ause to srot everything
		DrawList _DrawList;

		#endregion //Members

		#region Methods
		
		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			graphics.IsFullScreen = true;

			_clock = new GameClock();
			_inputState = new InputState();
			_inputWrapper = new InputWrapper(new ControllerWrapper(PlayerIndex.One, true), _clock.GetCurrentTime);
			_inputWrapper.Controller.UseKeyboard = true;
			_font = new FontBuddy();

			//setup the renderer
			_Renderer = new XNARenderer(this);
			_DrawList = new DrawList();
			_Grid = new Grid();
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			_clock.Start();

			base.Initialize();
				
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load all of your content.
		/// </summary>
		protected override void LoadContent()
		{
			_Renderer.LoadContent(graphics.GraphicsDevice);

			_Grid.LoadContent(_Renderer.Content);

			_font.LoadContent(Content, "ArialBlack24");
		}

		/// <summary>
		/// UnloadContent will be called once per game and is the place to unlaod all of your content.
		/// </summary>
		protected override void UnloadContent()
		{
			_Renderer.UnloadGraphicsContent();

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// Allows the game to exit
			if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) || 
			    Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				this.Exit();
			}

			//update the timer
			_clock.Update(gameTime);

			//update the input
			_inputState.Update();
			_inputWrapper.Update(_inputState, false);

			_Grid.Update(_inputWrapper);

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

			//Clear out the drawlist
			_DrawList.Flush();

			//Add all the grid textures to the drawlist
			Vector2 gridPos = new Vector2(300, 200);
			for (int i = 0; i < 4; i++)
			{
				_DrawList.AddQuad(_Grid.Grids[i],
				                  gridPos,
				                  _Grid.GridColors[i],
				                  0.0f, false,
				                  _Grid.Levels[i]);
				gridPos.X += 4;
				gridPos.Y += 4;
			}

			_Renderer.SpriteBatchBegin(BlendState.AlphaBlend, Matrix.Identity);

			_DrawList.Render(_Renderer);

			//Write the current layers
			Vector2 textPos = new Vector2(0, 0);
			for (int i = 0; i < 4; i++)
			{
				string gridText = _Grid.Levels[i].ToString();
				_font.Write(gridText,
				            textPos, 
				            Justify.Left,
				            1.0f,
				            _Grid.GridColors[i],
				            _Renderer.SpriteBatch,
				            0.0f);

				textPos.Y += _font.Font.MeasureString(gridText).Y;
			}

			_Renderer.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}

