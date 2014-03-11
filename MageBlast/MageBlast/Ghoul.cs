using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace MageBlast
{
    class Ghoul : Enemy
    {
        /* CONSTRUCTOR */
        public Ghoul(Game game, Point location, Rectangle boundaries)
            : base(game, location, boundaries, 10)
        { }

        /* METHODS */
        public override void Move(Random random)
        {
            if (NearPlayer())
            { game.HitPlayer(4, random); }
            else if (random.Next(1, 10) <= 5)
            { location = Move(FindPlayerDirection(game.PlayerLocation), game.Boundaries); }
        }
    }
}
