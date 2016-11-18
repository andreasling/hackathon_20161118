using System;
namespace fliptris.core
{
	public class Tetromino
	{
		public Position Position { get; private set; }
		public int[,] Parts { get; }

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

		public void Move(int dx, int dy)
		{
			Position = Position.Move(dx, dy);
		}
	}
}
