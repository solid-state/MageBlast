using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    class Arc : Spell
    {
        /* FIELDS */
        private int range = 55;
        private int damage = 3;

        /* PROPERTIES */
        public override string Name { get { return "Arc"; } }

        /* CONSTRUCTOR */
        public Arc(Game game, Point location)
            : base(game, location) { }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            switch (direction)
            {
                case Direction.Up:
                    DamageEnemy(Direction.Up, range, damage, random);
                    DamageEnemy(Direction.Left, range, damage, random);
                    DamageEnemy(Direction.Right, range, damage, random);
                    break;
                case Direction.Left:
                    DamageEnemy(Direction.Left, range, damage, random);
                    DamageEnemy(Direction.Up, range, damage, random);
                    DamageEnemy(Direction.Down, range, damage, random);
                    break;
                case Direction.Right:
                    DamageEnemy(Direction.Right, range, damage, random);
                    DamageEnemy(Direction.Up, range, damage, random);
                    DamageEnemy(Direction.Down, range, damage, random);
                    break;
                case Direction.Down:
                    DamageEnemy(Direction.Down, range, damage, random);
                    DamageEnemy(Direction.Left, range, damage, random);
                    DamageEnemy(Direction.Right, range, damage, random);
                    break;
                default: break;
            }
        }
        public override void Sound()
        {
            game.attackSnd.URL = @"audio\arc.wav";
            game.attackSnd.controls.play();
        }
    }
}
