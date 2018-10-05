using System;
using System.Drawing;

namespace AnatomyLabeller
{
	public class Label
	{
		private const int radius = 10;

		private const int diameter = radius * 2;

		private string name = "";

		private int id = -1;

		private Point originPosition;

		private Point drawPosition;

		public Label ( )
		{
			originPosition = new Point ( );

			drawPosition = new Point ( );
		}

		public Label ( int id , string _name , Point _position = new Point ( ) )
		{
			Name = _name;

			ID = id;

			originPosition = _position;

			//Subtract radius because we want the position to represent the center
			//of the circle, but we have to draw it as a square
			drawPosition.X = originPosition.X - radius;

			drawPosition.Y = originPosition.Y - radius;
		}

		public Point Origin
		{
			get
			{
				return originPosition;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public int ID
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		/// <summary>
		/// Gets the rectangle to draw in
		/// </summary>
		/// <returns>The rectangle representing the draw area</returns>
		public Rectangle GetDrawingRectangle ( )
		{
			return new Rectangle ( drawPosition.X , drawPosition.Y , diameter , diameter );
		}

		public void SetPosition ( Point _position )
		{
			originPosition = _position;

			//Subtract radius because we want the position to represent the center
			//of the circle, but we have to draw it as a square
			drawPosition.X = originPosition.X - radius;

			drawPosition.Y = originPosition.Y - radius;
		}

		public bool MouseClick ( Point mousePosition )
		{
			PointF difference = new PointF ( originPosition.X - mousePosition.X , originPosition.Y - mousePosition.Y );

			float magnitude = ( float ) Math.Sqrt ( ( difference.X * difference.X ) + ( difference.Y * difference.Y ) );

			return magnitude <= radius;
		}
	}
}