using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    class GreenPotion : Spell, IPotion
    {
        /* PROPERTIES */
        public override string Name { get { return "GreenPotion"; } }
        public bool Used { get; private set; }

        /* CONSTRUCTOR */
        public GreenPotion(Game game, Point location)
            : base(game, location) { Used = false; }

        /* METHODS */
        public override void Attack(Direction direction, Random random)
        {
            game.IncreasePlayerHealth(10, random);
            Used = true;
        }
        public override void Sound()
        {
            game.sounds.URL = @"audio\heal.wav";
            game.sounds.controls.play();
        }
    }
}
