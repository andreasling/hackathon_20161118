using System;
using System.Collections.Generic;
using System.Linq;

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
									if (parts[px, py] > 0)
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
			Spawn(Tetromino.Spawn(new Position(Width / 2, Height / 2)));
		}

		public void Spawn(Tetromino tetromino)
		{
			if (activeTetromino == null)
				activeTetromino = tetromino;
			else
				throw new InvalidOperationException();
		}

		public MoveResult Move()
		{
			return Move(0, 1);
		}

		public MoveResult Move(int dx, int dy)
		{
			if (activeTetromino != null)
			{
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
							if ((x < 0 || x >= Width || y < 0 || y >= Height) || parts[x,y] > 0)
							{
								collision = true;
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

					activeTetromino = null;

					return new MoveResult { IsGameOver = false, RemovedParts = Enumerable.Empty<Position>(), DidMove = false, GotStuck = true};
				}
				else
				{

					activeTetromino.Move(dx, dy);

					return new MoveResult { IsGameOver = false, RemovedParts = Enumerable.Empty<Position>(), DidMove = true, GotStuck = false };
				}
			}
			else
			{
				var removeRows = new List<int>();

				for (int y = 0; y < Height; y++)
				{
					var all = true;

					for (int x = 0; x < Width; x++)
					{
						if (!(all &= parts[x, y] > 0))
							break;
					}

					if (all)
					{
						removeRows.Add(y);
					}
				}

				if (removeRows.Any())
				{
					var removedParts = new List<Position>();
					foreach (var removeRow in removeRows)
					{
						var newParts = new int[Width, Height];

						for (int x = 0; x < Width; x++)
						{
							for (int y = 0; y < Height; y++)
							{
								if (y == removeRow)
									removedParts.Add(new Position(x, y));
								
								var oy = (y >= removeRow) ? y + 1 : y;
								if (oy < Height)
								{
									newParts[x, y] = parts[x, oy];
								}
							}
						}

						parts = newParts;
					}

					return new MoveResult { IsGameOver = false,  RemovedParts = removedParts, DidMove = false, GotStuck = false };
				}

				var removeCols = new List<int>();

				for (int x = 0; x < Width; x++)
				{
					var all = true;

					for (int y = 0; y < Height; y++)
					{
						if (!(all &= parts[x, y] > 0))
							break;
					}

					if (all)
					{
						removeCols.Add(x);
					}
				}

				if (removeCols.Any())
				{
					var removedParts = new List<Position>();
					foreach (var removeCol in removeCols)
					{
						var newParts = new int[Width, Height];

						for (int y = 0; y < Height; y++)
						{
							for (int x = 0; x < Width; x++)
							{
								if (x == removeCol)
									removedParts.Add(new Position(x, y));
								var ox = (x >= removeCol) ? x + 1 : x;
								if (ox < Width)
								{
									newParts[x, y] = parts[ox, y];
								}
							}
						}

						parts = newParts;
					}

					return new MoveResult { IsGameOver = false, RemovedParts = removedParts, DidMove = false, GotStuck = false };
				}


				Spawn();
				return new MoveResult { IsGameOver = false, RemovedParts = Enumerable.Empty<Position>(), DidMove = false, GotStuck = false };
			}

			return new MoveResult { IsGameOver = false, RemovedParts = Enumerable.Empty<Position>(), DidMove = false, GotStuck = false };
		}
	}
}
