using NUnit.Framework;
using System;
using System.Collections.Generic;
using fliptris.core;

namespace fliptris.tests
{
	[TestFixture()]
	public class Test
	{
		[Test()]
		public void ShouldCreateBoard()
		{
			var width = 16;
			var height = 16;
			var board = new Board(width, height);

			Assert.That(board.Width, Is.EqualTo(width));
			Assert.That(board.Height, Is.EqualTo(height));
		}

		[Test()]
		public void ShouldGetBoardState()
		{
			var width = 16;
			var height = 16;
			var board = new Board(width, height);

			var state = board.State;

			var expected = new int[width, height];

			Assert.That(state, Is.EquivalentTo(expected));

		}

		[Test]
		public void ShouldSpawnPiece()
		{
			var width = 4;
			var height = 4;
			var board = new Board(width, height);

			var piece = new Tetromino();

			board.Spawn(piece);

			var state = board.State;

			var expectedState = new int[,] {
				{ 1, 0, 0, 0},
				{ 0, 0, 0, 0},
				{ 0, 0, 0, 0},
				{ 0, 0, 0, 0}
			};

			Assert.That(state, Is.EqualTo(expectedState));
		}

		[Test]
		public void ShouldMoveActiveTetromino()
		{
			var width = 4;
			var height = 4;
			var board = new Board(width, height);

			var piece = new Tetromino();

			board.Spawn(piece);

			var couldMove = board.Move();

			var state = board.State;

			var expectedState = new int[,] {
				{ 0, 1, 0, 0},
				{ 0, 0, 0, 0},
				{ 0, 0, 0, 0},
				{ 0, 0, 0, 0}
			};

			Assert.That(couldMove, Is.True);
			Assert.That(state, Is.EqualTo(expectedState));
		}


		[Test]
		public void ShouldStickActiveTetromino()
		{
			var width = 4;
			var height = 4;
			var board = new Board(width, height);

			var piece = new Tetromino();

			board.Spawn(piece);

			board.Move();
			board.Move();
			board.Move();
			var couldMove = board.Move();

			var state = board.State;

			var expectedState = new int[,] {
				{ 0, 0, 0, 1},
				{ 0, 0, 0, 0},
				{ 0, 0, 0, 0},
				{ 0, 0, 0, 0}
			};

			Assert.That(couldMove, Is.False);
			Assert.That(state, Is.EqualTo(expectedState));
		}

		[Test]
		public void ShouldCreatePiece()
		{
			var parts = new int[,] { { 1 } };

			var piece = new Tetromino(parts);

			var expected = new int[,] { { 1 } };

			Assert.That(piece.Parts, Is.EqualTo(expected));
		}

		[Test]
		public void ShouldMovePiece()
		{
			var piece = new Tetromino();

			Assert.That(piece.Position, Is.EqualTo(Position.Zero));

			piece.Move(1,0);

			var expected = new Position(1,0);

			Assert.That(piece.Position, Is.EqualTo(expected));
		}

		[Test]
		public void ShouldCreatePosition()
		{
			var position = new Position(0, 0);

			Assert.That(position.X, Is.EqualTo(0));
			Assert.That(position.Y, Is.EqualTo(0));
		}
	
		[Test]
		public void ShouldHavePositionZero()
		{
			Assert.That(Position.Zero.X, Is.EqualTo(0));
			Assert.That(Position.Zero.Y, Is.EqualTo(0));
		}

		[Test]
		public void ShouldMovePosition()
		{
			var position = Position.Zero;
			var newPosition = position.Move(1,0);
			Assert.That(position.X, Is.EqualTo(0));
			Assert.That(position.Y, Is.EqualTo(0));
			Assert.That(newPosition.X, Is.EqualTo(1));
			Assert.That(newPosition.Y, Is.EqualTo(0));
		}

		/* [Test]
		public void ShouldCollideParts()
		{
			Assert.That(Collide(new int[,] { { 1 } }, new int[,] { { 1 } }), Is.True);
		}

		public bool Collide(int[,] first, int[,] second)
		{
			var x_max = Math.Max(first.GetLength(0), second.GetLength(0));
			var y_max = Math.Max(first.GetLength(1), second.GetLength(1));
			for (int x = 0; x < x_max; x++)
			{
				for (int y = 0; y < y_max; y++)
				{
					
				}
			}
		} */
	}
}
