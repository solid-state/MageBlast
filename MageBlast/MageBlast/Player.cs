using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace MageBlast
{
    class Player : Movement
    {
        /*  FIELD VARIABLES */
        private Spell equippedSpell;
        
        /* PROPERTIES */
        private int hitPoints;
        public int HitPoints { get { return hitPoints; } }
        private List<Spell> inventory = new List<Spell>();
        public List<string> Spells
        { get {
            List<string> names = new List<string>();
            foreach (Spell spell in inventory)
                names.Add(spell.Name);
            return names; }
        }

        /* CONSTRUCTOR */
        public Player(Game game, Point location, Rectangle boundaries)
            : base(game, location)
        {
            hitPoints = 20;
        }

        /* METHODS */
        public void Heal(int hp)
        {
            hitPoints += hp;
        }
        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            hitPoints += random.Next(1, health);
        }

        public void Equip(string spellName)
        {
            foreach (Spell spell in inventory)
            {
                if (spell.Name == spellName)
                    equippedSpell = spell;
            }
        }

        public void Move(Direction direction)
        {
            foreach (Enemy enemy in game.Enemies)
            {
                switch (direction)
                {
                    case Direction.Up:
                        if (!(Nearby(enemy.Location, Location, 55) == Direction.Down)) // top
                        {
                            base.location = Move(direction, game.Boundaries);
                        }
                        break;
                    case Direction.Down:
                        if (!(Nearby(enemy.Location, Location, 55) == Direction.Up)) // bottom
                        {
                            base.location = Move(direction, game.Boundaries);
                        }
                        break;
                        case Direction.Right:
                        if (!(Nearby(enemy.Location, Location, 55) == Direction.Left)) 
                        {
                            base.location = Move(direction, game.Boundaries);
                        }
                        break;
                    case Direction.Left:
                        if (!(Nearby(enemy.Location, Location, 55) == Direction.Right)) 
                        {
                            base.location = Move(direction, game.Boundaries);
                        }
                        break;
                    default:
                        base.location = Move(direction, game.Boundaries); // Direction.Home  = new game
                        break;
               }
            }
            if (!game.SpellInRoom.PickedUp)
            {
                if (!(Nearby(game.SpellInRoom.Location, 10) == Direction.Home))  
                {
                    game.sounds.URL = @"audio\pickup.wav";
                    game.sounds.controls.play();
                    game.SpellInRoom.PickUpWeapon();
                    inventory.Add(game.SpellInRoom);
                }
            }
        }

        public void Attack(Direction direction, Random random)
        {
            if (equippedSpell != null) 
            {
                equippedSpell.Sound();
                if (!(equippedSpell is IPotion))
                {
                    equippedSpell.Attack(direction, random);

                }
                if (equippedSpell is IPotion)
                {
                    if (equippedSpell.Name == "BluePotion")
                        IncreaseHealth(10, random); // Add 10 health points
                    if (equippedSpell.Name == "GreenPotion")
                        hitPoints = 20;        // Full health
                    inventory.Remove(equippedSpell);       // Take away used potion
                }
            }
            else
                MessageBox.Show("You have no spell armed!");
        }   // End of Attack method

        public bool PotonUsed(string potionName)
        {
            IPotion potion;
            bool potionUsed = true;
            foreach (Spell spell in inventory)
            {
                if (spell.Name == potionName)
                {
                    potion = spell as IPotion;
                    potionUsed = potion.Used;
                }
            }
            return potionUsed;
        }


    }
}
