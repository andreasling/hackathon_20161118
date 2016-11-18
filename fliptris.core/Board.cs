using System;
using System.Collections.Generic;

namespace fliptris.core
{
	public class Board
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		private int[,] parts;
		private Tetromino activeTetromino = null;

		public Board(int width, int height)
		{
			Width = width;
			Height = height;

			parts = new int[width, height];
		}

		public int[,] State
		{
			get
			{
				var state = new int[Width, Height];

				for (int x = 0; x < Width; x++)
				{
					for (int y = 0; y < Height; y++)
					{
						state[x, y] = parts[x, y];
					}
				}

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
				var dx = 0;
				var dy = 1;

				var tetromino_parts = activeTetromino.Parts;

				var collision = false;

				for (int px = 0; px < tetromino_parts.GetLength(0); px++)
				{
					var x = px + activeTetromino.Position.X + dx;

					for (int py = 0; py < tetromino_parts.GetLength(1); py++)
					{
						var y = py + activeTetromino.Position.Y + dy;

						if (tetromino_parts[px, py] > 0)
						{
							if ((x < 0 || x >= Width || y < 0 || y >= Height))
							{
								
								collision = true;;
							}
						}
					}
				}

				if (collision)
				{
					for (int px = 0; px < tetromino_parts.GetLength(0); px++)
					{
						var x = px + activeTetromino.Position.X;

						for (int py = 0; py < tetromino_parts.GetLength(1); py++)
						{
							var y = py + activeTetromino.Position.Y;

							if (tetromino_parts[px, py] > 0)
							{
								/* if ((x < 0 || x >= Width || y < 0 || y >= Height))
								{

									collision = true; ;
								} */
								parts[x, y] = tetromino_parts[px, py];
							}
						}
					}

					return false;
				}
				else
				{

					activeTetromino.Move(dx, dy);

					return true;
				}
			}

            //var result = new MoveResult();

            //var removedParts = new List<Position>();


            //result.IsGameOver = false;
            //result.RemovedParts = removedParts;

            //return result;

            return false;
		}
	}
}
