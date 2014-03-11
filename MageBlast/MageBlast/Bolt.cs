using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    class Bolt : Spell
    {
        /* FIELDS */
        private int range = 165;
        private int damage = 1;

        /* PROPERTIES */
        public override string Name { get { return "Bolt"; } }

        /* CONSTRUCTOR */
        public Bolt(Game game, Point location)
            : base(game, location) { }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            switch (direction)
            {
                case Direction.Up:
                    DamageEnemy(Direction.Up, range, damage, random);
                    break;
                case Direction.Left:
                    DamageEnemy(Direction.Left, range, damage, random);
                    break;
                case Direction.Right:
                    DamageEnemy(Direction.Right, range, damage, random);
                    break;
                case Direction.Down:
                    DamageEnemy(Direction.Down, range, damage, random);
                    break;
                default: break;
            }
        }
        public override void Sound()
        {
            game.attackSnd.URL = @"audio\bolt.wav";
            game.attackSnd.controls.play();
        }
    }
}

