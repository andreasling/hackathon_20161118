using System;
namespace fliptris.core
{
	public class Position
	{
		public int X { get; private set; }
		public int Y { get; private set; }

		public static Position Zero = new Position(0, 0);

		public Position(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals(object obj)
		{
			return obj is Position
				? this.Equals(obj as Position)
				: base.Equals(obj);
		}

		private bool Equals(Position that)
		{
			return this.X == that.X && this.Y == that.Y;
		}

		public Position Move(int dx, int dy)
		{
			return new Position(X + dx, Y + dy);
		}
	}
}
