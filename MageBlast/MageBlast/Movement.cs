using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    public enum Direction
    {
        Home = 0,
        Left = -1,
        Right = 1,
        Up = -2,
        Down = 2,   
    }

    abstract class Movement
    {
        /*  FIELD VARIABLES */
        public const int movementX = 51; // Movement of charactor 
        //51 x 10 = 510
        public const int movementY = 49; // Grid squares must not be square! or trick of Framework system
        // 49 x 5 = 245
        protected Game game;

        /* PROPERTIES */
        protected Point location;
        public Point Location { get { return location; } }
        private Direction face = Direction.Up;
        public Direction Face
        {
            get { return face; }
            set { face = value; }
        }
        /* CONSTRUCTOR */
        public Movement(Game game, Point location)
        {
            this.game = game;
            this.location = location;
        }

        /* METHODS */
        public Direction Nearby(Point locationToCheak, int distance)  // Used by Enemy class + player.PickedUp
        {
            if (Math.Abs(location.X - locationToCheak.X) < distance &&
                (Math.Abs(location.Y - locationToCheak.Y) < distance))
            { return Direction.Up; }
            else
            { return Direction.Home; }
        }

        public Direction Nearby(Point locationToCheak, Point location, int distance) // Used by Player class
        {
            if ((location.X < locationToCheak.X) && (Math.Abs(location.X - locationToCheak.X) < distance) &&
                (Math.Abs(location.Y - locationToCheak.Y) <= 10))  // Left of Enemy
            { return Direction.Left; }
            else if ((location.X > locationToCheak.X) && (Math.Abs(location.X - locationToCheak.X) < distance) &&
                (Math.Abs(location.Y - locationToCheak.Y) <= 10))  // Right of Enemy
            { return Direction.Right; }
            else if ((location.Y < locationToCheak.Y) && (Math.Abs(location.Y - locationToCheak.Y) < distance) &&
                (Math.Abs(location.X - locationToCheak.X) <= 2))  // UP of Enemy
            { return Direction.Up; }
            else if ((location.Y > locationToCheak.Y) && (Math.Abs(location.Y - locationToCheak.Y) < distance) &&
                (Math.Abs(location.X - locationToCheak.X) <= 2))      // DOWN of Enemy
            { return Direction.Down; }
            else
            { return Direction.Home; }
        }

        public Point Move(Direction direction, Rectangle boundaries)
        {
            Point newLocation = location;  // Position of Movement (Player, Enemy, or Spell)
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y >= boundaries.Y) // top
                    {
                        newLocation.Y -= movementY;
                        Face = Direction.Up;
                    }
                    break;
                case Direction.Down:
                    if (newLocation.Y + movementY <= boundaries.Height)
                    {
                        newLocation.Y += movementY;
                        Face = Direction.Up;
                    }
                    break;
                case Direction.Left:
                    if (newLocation.X - movementX >= boundaries.X)
                    {
                        newLocation.X -= movementX;
                        Face = Direction.Up;
                    }
                    break;
                case Direction.Right:
                    if (newLocation.X <= boundaries.Width) //Right
                    {
                        newLocation.X += movementX;
                        Face = Direction.Up;
                    }
                    break;
                case Direction.Home:        // Starting position of player
                    newLocation.X = 52;
                    newLocation.Y = 127;
                    Face = Direction.Up;
                    break;
                default: break;
            }
            return newLocation;
        }
    }
}
