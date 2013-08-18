using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using HadoukInput;
using DrawListBuddy;

namespace DrawListBuddySample
{
	public class Grid
	{
		#region Members

		//The four grids we are gonna display
		public List<Texture2D> Grids { get; private set; }

		//the levels of the four grids
		public List<int> Levels { get; private set; }

		/// <summary>
		/// the colors to darw each grid
		/// </summary>
		/// <value>The levels.</value>
		public List<Color> GridColors { get; private set; }

		#endregion //Members

		#region Methods

		public Grid()
		{
			Levels = new List<int>();
			Grids = new List<Texture2D>();
			GridColors = new List<Color>();
		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		public void LoadContent(ContentManager content)
		{
			//load four sets of stuff
			for (int i = 0; i < 4; i++)
			{
				//load four grids
				Grids.Add(content.Load<Texture2D>("grid.png"));

				//add four 0's for their levels
				Levels.Add(0);
			}

			GridColors.Add(Color.Red);
			GridColors.Add(Color.Yellow);
			GridColors.Add(Color.DarkGreen);
			GridColors.Add(Color.Blue);
		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		public void Update(InputWrapper input)
		{
			if (input.Controller.KeystrokePress[(int)EKeystroke.A])
			{
				Levels[0]--;
			}

			if (input.Controller.KeystrokePress[(int)EKeystroke.B])
			{
				Levels[1]--;
			}

			if (input.Controller.KeystrokePress[(int)EKeystroke.X])
			{
				Levels[2]--;
			}

			if (input.Controller.KeystrokePress[(int)EKeystroke.Y])
			{
				Levels[3]--;
			}
		}

		#endregion //Methods
	}
}

