using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    class Sphere : Spell
    {
        /* FIELDS */
        private int range = 55;
        private int damage = 6;

        /* PROPERTIES */
        public override string Name { get { return "Sphere"; } }

        /* CONSTRUCTOR */
        public Sphere(Game game, Point location)
            : base(game, location) { }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            DamageEnemy(Direction.Up, range, damage, random);
            DamageEnemy(Direction.Left, range, damage, random);
            DamageEnemy(Direction.Right, range, damage, random);
            DamageEnemy(Direction.Down, range, damage, random);
        }

        public override void Sound()
        {
            game.attackSnd.URL = @"audio\sphere.wav";
            game.attackSnd.controls.play();
        }
    }
}
