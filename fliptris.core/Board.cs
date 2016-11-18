using System;
using System.Collections.Generic;

namespace fliptris.core
{
	public class Board
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		private Tetromino activeTetromino = null;

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

				if (activeTetromino != null)
				{
					var parts = activeTetromino.Parts;

					for (int x = 0; x < Width; x++)
					{
						var px = x - activeTetromino.Position.X;
						if (px >= 0 && px < parts.GetLength(0))
						{
							for (int y = 0; y < Height; y++)
							{
								var py = y - activeTetromino.Position.Y;
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
			if (activeTetromino == null)
				activeTetromino = tetromino;
			else
				throw new InvalidOperationException();
		}

		public bool Move()
		{
			if (activeTetromino != null)
			{
				activeTetromino.Move(0, 1);

				/* var parts = activeTetromino.Parts;

				for (int px = 0; px < parts.GetLength(0); px++)
				{
					var x = px + activeTetromino.Position.X;

					for (int py = 0; py < parts.GetLength(1); py++)
					{
						var y = py + activeTetromino.Position.Y;

						if (parts[px, py] > 0)
						{
							if ((x < 0 || x >= Width))
							{
								// todo: stick
								return false;
							}
							else
							{
								if (y < 0 || y >= Height)
								{
									// todo: stick
									return false;
								}
							}
						}
					}
				}
				*/
				return true;

			}

            //var result = new MoveResult();

            //var removedParts = new List<Position>();
            

            //result.IsGameOver = false;
            //result.RemovedParts = removedParts;

            //return result;
		}
	}
}
