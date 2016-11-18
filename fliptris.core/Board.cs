using System;
using System.Collections.Generic;

namespace fliptris.core
{
	public class Board
	{

		public int Width { get; private set; }
		public int Height { get; private set; }

		private List<Tetromino> pieces = new List<Tetromino>();

		public Board(int width, int height)
		{
			Width = width;
			Height = height;
		}

		public int[,] State
		{
			get
			{
				var state = new int[Width, Height];

				foreach (var piece in pieces)
				{
					var parts = piece.Parts;

					for (int x = 0; x < Width; x++)
					{
						var px = x - piece.Position.X;
						if (px >= 0 && px < parts.GetLength(0))
						{
							for (int y = 0; y < Height; y++)
							{
								var py = y - piece.Position.Y;
								if (py >= 0 && py < parts.GetLength(1))
								{
									state[x, y] = parts[px, py];
								}
							}

						}
					}
				}

				return state;
			}
		}

		public void Spawn()
		{
			Spawn(Tetromino.Spawn());
		}

		public void Spawn(Tetromino tetromino)
		{
			pieces.Add(tetromino);
		}

		public void Move()
		{
			foreach (var piece in pieces)
			{
				//piece.Position.Y += 1;
				piece.Move(0, 1);
			}
		}
	}
}
