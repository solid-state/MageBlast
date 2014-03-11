using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MageBlast
{
    abstract class Enemy : Movement
    {
        /*  FIELD VARIABLES */
        private const int nearPlayerDistance = 55;

        /* PROPERTIES */
        private int hitPoints;
        public int HitPoints { get { return hitPoints; } }

        public bool Dead
        {   get {
                if (hitPoints <= 0) return true;
                else return false;
                }
        }

        /* CONSTRUCTOR */
        public Enemy(Game game, Point location, Rectangle boundaries, int hitPoints)
            : base(game, location) { this.hitPoints = hitPoints; }

        /* METHODS */
        public abstract void Move(Random random);

        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1, maxDamage); 
        }

        protected bool NearPlayer()
        {
            if (Nearby(game.PlayerLocation, nearPlayerDistance) == Direction.Home)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        protected Direction FindPlayerDirection(Point playerLocation)
        {
            Direction directionToMove;
            if (playerLocation.X > location.X + movementX)
                directionToMove = Direction.Right;
            else if (playerLocation.X < location.X - movementX)
                directionToMove = Direction.Left;
            else if (playerLocation.Y < location.Y - movementY)
                directionToMove = Direction.Up;
            else
                directionToMove = Direction.Down;
            return directionToMove;
        }
    }
}
