using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    abstract class Spell : Movement
    {
        /*  FIELD VARIABLES */
        //protected Game game;

        /* PROPERTIES */
        private bool pickedUp;
        public bool PickedUp { get { return pickedUp; } }
        public abstract string Name { get; }

        /* CONSTRUCTOR */
        public Spell(Game game, Point location)
            : base(game, location)
        { pickedUp = false; }

        /* METHODS */
        public void PickUpWeapon() { pickedUp = true; }

        public abstract void Attack(Direction direction, Random random);
        public abstract void Sound();

        protected bool DamageEnemy(Direction direction, int range, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < range; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    if (enemy.HitPoints > 0 && (!(Nearby(enemy.Location, target, range) == Direction.Home)))// If in localaty, damage enemy
                    {
                        enemy.Hit(damage, random); // The method that subtracts damage from enemy
                        return true;
                    }
                }
                target = Move(direction, game.Boundaries);
            }
            return false;
        }
    }
}
