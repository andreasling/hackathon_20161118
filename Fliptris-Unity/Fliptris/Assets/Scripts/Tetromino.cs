using System;
namespace fliptris.core
{
	public class Tetromino
	{
		public Position Position { get; private set; }
		public int[,] Parts { get; private set; }

		private static Tetromino I = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 1, 1, 1, 1 },
			{ 0, 0, 0, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino J = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 2, 2, 2, 0 },
			{ 0, 0, 2, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino L = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 0, 3, 3, 3 },
			{ 0, 3, 0, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino O = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 0, 4, 4, 0 },
			{ 0, 4, 4, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino S = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 0, 5, 5, 0 },
			{ 5, 5, 0, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino T = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 6, 6, 6, 0 },
			{ 0, 6, 0, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino Z = new Tetromino(new[,] {
			{ 0, 0, 0, 0 },
			{ 7, 7, 0, 0 },
			{ 0, 7, 7, 0 },
			{ 0, 0, 0, 0 }
		});
		private static Tetromino[] tetrominos = new[] { I, J, L, O, S, T, Z };

		public Tetromino()
			: this(new int[,] { { 1 } })
		{
			Position = Position.Zero;
			Parts = new int[,] { { 1 } };
		}

		public Tetromino(int[,] parts)
			: this(parts, Position.Zero)
		{

		}

		public Tetromino(int[,] parts, Position position)
		{
			Position = position;
			Parts = parts;
		}

		public static Tetromino Spawn()
		{
			return Spawn(Position.Zero);
		}

		public static Tetromino Spawn(Position position)
		{
			var r = new Random().Next(7);
			var proto = tetrominos[r];


			return new Tetromino(proto.Parts, new Position(position.X - 2, position.Y - 2));
		}

		public void Move(int dx, int dy)
		{
			Position = Position.Move(dx, dy);
		}
	}
}
