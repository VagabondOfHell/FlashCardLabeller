using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnatomyLabeller
{
    public class Label
    {
        Point originPosition;
        Point drawPosition;

        const int radius = 10;
        const int diameter = radius * 2;

        string name;
        int ID;

        public Label()
        {
            name = "";
            ID = -1;
            originPosition = new Point();
            drawPosition = new Point();
        }

        public Label(int id, string _name, Point _position = new Point())
        {
            name = _name;

            ID = id;

            originPosition = _position;
            //Subtract radius because we want the position to represent the center
            //of the circle, but we have to draw it as a square
            drawPosition.X = originPosition.X - radius;
            drawPosition.Y = originPosition.Y - radius;
        }

        public Point GetOrigin()
        {
            return originPosition;
        }

        public string GetName()
        {
            return name;
        }

        public int GetID()
        {
            return ID;
        }

        public void SetID(uint id)
        {
            ID = (int)id;
        }

        public void SetName(string _name)
        {
            name = _name;
        }

        /// <summary>
        /// Gets the rectangle to draw in
        /// </summary>
        /// <returns>The rectangle representing the draw area</returns>
        public Rectangle GetDrawingRectangle()
        {
            return new Rectangle(drawPosition.X, drawPosition.Y,diameter, diameter);
        }

        public void SetPosition(Point _position)
        {
            originPosition = _position;

            //Subtract radius because we want the position to represent the center
            //of the circle, but we have to draw it as a square
            drawPosition.X = originPosition.X - radius;
            drawPosition.Y = originPosition.Y - radius;
        }

        public bool MouseClick(Point mousePosition)
        {
            PointF difference = new PointF(originPosition.X - mousePosition.X, originPosition.Y - mousePosition.Y);
            float magnitude = (float)Math.Sqrt((difference.X * difference.X) + (difference.Y * difference.Y));

            return magnitude <= radius;     
        }

    }
}
