using System;

namespace Tiler
{
	public struct Point
	{
		/// <summary>
		/// A point at the origin (0, 0).
		/// </summary>
		public static readonly Point Zero = new Point(0, 0);

		/// <summary>
		/// The X component of the Point.
		/// </summary>
		public int X;

		/// <summary>
		/// The Y component of the Point.
		/// </summary>
		public int Y;

		/// <summary>
		/// Gets or sets the value at the index of the Point.
		/// </summary>
		public int this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.X;
					case 1: return this.Y;
					default: throw new IndexOutOfRangeException("Point access at index: " + index);
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.X = value; return;
					case 1: this.Y = value; return;
					default: throw new IndexOutOfRangeException("Point access at index: " + index);
				}
			}
		}

		/// <summary>
		/// Constructs a new Point.
		/// </summary>
		/// <param name="x">The x coordinate of the Point.</param>
		/// <param name="y">The y coordinate of the Point.</param>
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Calculate the component-wise minimum of two points.
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <returns>The component-wise minimum</returns>
		public static Point Min(Point a, Point b)
		{
			a.X = a.X < b.X ? a.X : b.X;
			a.Y = a.Y < b.Y ? a.Y : b.Y;
			return a;
		}

		/// <summary>
		/// Calculate the component-wise maximum of two points.
		/// </summary>
		/// <param name="a">First operand</param>
		/// <param name="b">Second operand</param>
		/// <returns>The component-wise maximum</returns>
		public static Point Max(Point a, Point b)
		{
			a.X = a.X > b.X ? a.X : b.X;
			a.Y = a.Y > b.Y ? a.Y : b.Y;
			return a;
		}

		/// <summary>
		/// Calculates the distance between two points described by two points.
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static float Distance(Point left, Point right)
		{
			Point diff;
			diff.X = left.X - right.X;
			diff.Y = left.Y - right.Y;
			return (float)Math.Sqrt((diff.X * diff.X) + diff.Y * diff.Y);
		}

		/// <summary>
		/// Adds the specified points component-wise.
		/// </summary>
		public static Point operator +(Point left, Point right)
		{
			return new Point(
				left.X + right.X,
				left.Y + right.Y);
		}

		/// <summary>
		/// Subtracts the specified points component-wise.
		/// </summary>
		public static Point operator -(Point left, Point right)
		{
			return new Point(
				left.X - right.X,
				left.Y - right.Y);
		}

		/// <summary>
		/// Inverts the specified point component-wise,
		/// </summary>
		public static Point operator -(Point point)
		{
			return new Point(
				-point.X,
				-point.Y);
		}

		/// <summary>
		/// Multiplies the specified point component-wise with the specified factor.
		/// </summary>
		public static Point operator *(Point left, int right)
		{
			return new Point(
				left.X * right,
				left.Y * right);
		}

		/// <summary>
		/// Multiplies the specified point component-wise with the specified factor.
		/// </summary>
		public static Point operator *(int left, Point right)
		{
			return right * left;
		}

		/// <summary>
		/// Multiplies the specified points component-wise.
		/// </summary>
		public static Point operator *(Point left, Point right)
		{
			return new Point(
				left.X * right.X,
				left.Y * right.Y);
		}

		/// <summary>
		/// Divides the specified point component-wise with the specified value.
		/// </summary>
		public static Point operator /(Point left, int right)
		{
			return new Point(
				left.X / right,
				left.Y / right);
		}

		/// <summary>
		/// Divides the specified points component-wise.
		/// </summary>
		public static Point operator /(Point left, Point right)
		{
			return new Point(
				left.X / right.X,
				left.Y / right.Y);
		}

		/// <summary>
		/// Compares the specified instances for equality.
		/// </summary>
		/// <param name="left">Left operand.</param>
		/// <param name="right">Right operand.</param>
		/// <returns>True if both instances are equal; false otherwise.</returns>
		public static bool operator ==(Point left, Point right)
		{
			return left.Equals(right);
		}

		/// <summary>
		/// Compares the specified instances for inequality.
		/// </summary>
		/// <param name="left">Left operand.</param>
		/// <param name="right">Right operand.</param>
		/// <returns>True if both instances are not equal; false otherwise.</returns>
		public static bool operator !=(Point left, Point right)
		{
			return !left.Equals(right);
		}

		/// <summary>
		/// Returns a System.String that represents the current Point.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return string.Format("({0}, {1})", this.X, this.Y);
		}

		/// <summary>
		/// Returns the hashcode for this instance.
		/// </summary>
		/// <returns>A System.Int32 containing the unique hashcode for this instance.</returns>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ ((this.Y << 16).GetHashCode());
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj">The object to compare to.</param>
		/// <returns>True if the instances are equal; false otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Point))
				return false;

			return this.Equals((Point)obj);
		}

		/// <summary>
		/// Indicates whether the current point is equal to another point.
		/// </summary>
		/// <param name="other">A point to compare with this point.</param>
		/// <returns>true if the current point is equal to the point parameter; otherwise, false.</returns>
		public bool Equals(Point other)
		{
			return
				this.X == other.X &&
				this.Y == other.Y;
		}
	}
}