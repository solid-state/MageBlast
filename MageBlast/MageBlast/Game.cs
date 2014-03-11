using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Media; // Declaring here and not in code speeds operation
using WMPLib; // Used as an extra sound player


namespace MageBlast
{
    public enum Limits
    {
        Top,
        Left,
        Right,
        Bottom,
    }

    class Game
    {
        /* FIELD OBJECTS*/
        public List<Enemy> Enemies;
        public Spell SpellInRoom;
        private Player player;
        public WindowsMediaPlayer backSnd;
        public WindowsMediaPlayer stepSnd;  // Used in Move() method
        public WindowsMediaPlayer attackSnd;
        public WindowsMediaPlayer sounds;
                
        /* PROPERTIES */
        public Point PlayerLocation { get { return player.Location; } }
        public int PlayerHitPoints { get { return player.HitPoints; } }
        public List<string> PlayerSpells { get { return player.Spells; } } // Not used
        
        private int level = 0;
        public int Level { get { return level; } }

        private Rectangle boundaries;   // Area of the limits of playable game
        public Rectangle Boundaries { get { return boundaries; } }
        
        /* CONSTRUCTOR */
        public Game(Rectangle boundaries)
        {
            this.boundaries = boundaries; 
            player = new Player(this, new Point(boundaries.X + 2, boundaries.Y + 97), 
                    boundaries);  // Starting Position of Character
            backSnd = new WindowsMediaPlayer();
            stepSnd = new WindowsMediaPlayer();
            attackSnd = new WindowsMediaPlayer();
            sounds = new WindowsMediaPlayer();
            
        }

        /* METHODS */
        public void Move(Direction direction) //No one uses except 'take me home'
        {
            player.Move(direction);
        }

        public void Move(Direction direction, Random random)
        {
            player.Move(direction);
            stepSnd.URL = @"audio\move.wav";
            stepSnd.controls.play();
            
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.HitPoints > 0)
                {
                    enemy.Move(random);
                }
            }
        }

        public void Attack(Direction direction, Random random)
        {
            player.Attack(direction, random);
            foreach (Enemy enemy in Enemies)
            {
                if (enemy.HitPoints > 0)
                {
                    enemy.Move(random);
                }
            }
        }

        private Point GetRandomLocation(Random random) // Starting position of entities
        {
            return new Point(Player.movementX * (random.Next(2, 10)),
                            (Player.movementY * (random.Next(2, 5))) - 15);
        }

        public void Equip(string spellName)        // Single line method, seems wasteful
        { player.Equip(spellName); }

        public bool CheckPlayerInventory(string spellName)
        {
            return player.Spells.Contains(spellName); 
        }

        public void HitPlayer(int maxDamage, Random random)
        {
            attackSnd.URL = @"audio\attack.wav";
            attackSnd.controls.play();
            player.Hit(maxDamage, random); 
        }

        public bool PotionUsed(string potionName)
        { return player.PotonUsed(potionName); }

        public void IncreasePlayerHealth(int health, Random random)
        { player.IncreaseHealth(health, random); }

        public void NewLevel(Random random)
        {
            
            level++;
            if (!(level == 8))
            {
                backSnd.URL = @"audio\start.wav";
                backSnd.controls.play();
            }
            SpellInRoom = null;
            switch (level)
            {
                case 1:
                    
                    Enemies = new List<Enemy>();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    SpellInRoom = new Arc(this, GetRandomLocation(random));
                    break;
                case 2:

                    Enemies.Clear();
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    SpellInRoom = new BluePotion(this, GetRandomLocation(random));
                    break;
                case 3:
                    Enemies.Clear();
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    SpellInRoom = new Bolt(this, GetRandomLocation(random));
                    break;
                case 4:
                    Enemies.Clear();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    if (player.Spells.Contains("Bolt"))
                        SpellInRoom = new BluePotion(this, GetRandomLocation(random));
                    else
                        SpellInRoom = new Bolt(this, GetRandomLocation(random));
                    break;
                case 5:
                    Enemies.Clear();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    SpellInRoom = new GreenPotion(this, GetRandomLocation(random));
                    break;
                case 6:
                    Enemies.Clear();
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    SpellInRoom = new Sphere(this, GetRandomLocation(random));
                    break;
                case 7:
                    Enemies.Clear();
                    Enemies.Add(new Bat(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghost(this, GetRandomLocation(random), boundaries));
                    Enemies.Add(new Ghoul(this, GetRandomLocation(random), boundaries));
                    if (player.Spells.Contains("Sphere"))
                        SpellInRoom = new GreenPotion(this, GetRandomLocation(random));
                    else
                        SpellInRoom = new Sphere(this, GetRandomLocation(random));
                    break;
                case 8:
                    Enemies.Clear();
                    stepSnd.URL = @"audio\win.wav";
                    stepSnd.controls.play();
                    MessageBox.Show("You have slain all known beasty, \nyou art hero of this land.");
                    Environment.Exit(0);
                    break;
                default: break;
            }
        }
    }
}
