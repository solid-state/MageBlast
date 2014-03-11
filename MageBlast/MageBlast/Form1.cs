using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;



namespace MageBlast
{
    public partial class Form1 : Form
    {

        private Game game;
        private Random random = new Random();
        public int round = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(new Rectangle(50, 30, 500, 250));
            game.NewLevel(random);
            picturePlayer.Visible = true;
            UpdateCharacters();
        }

        /* UpdateCharacters Method */
        public void UpdateCharacters()
        {
            if (round == 0)
            { game.Move(Direction.Home); }
            picturePlayer.Location = game.PlayerLocation;

            /* Hide Weapon icon PictureBoxes */
            pictureArc.Visible = false;
            pictureBolt.Visible = false;
            pictureSphere.Visible = false;
            pictureRed.Visible = false;
            pictureBlue.Visible = false;
            playerHitPoint.Text = game.PlayerHitPoints.ToString();
            label6.Text = game.Level.ToString();
            round++;
            label10.Text = round.ToString();
            int enemiesShown = 0;   // This is reset each time UpdateCharacters() method is called

            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    pictureBat.Location = enemy.Location;
                    batHitPoint.Text = enemy.HitPoints.ToString();
                    pictureBat.Visible = true;
                    if (enemy.HitPoints > 0)
                    {
                        enemiesShown++;
                    }
                    else
                    {
                        pictureBat.Visible = false;
                    }
                }
                if (enemy is Ghost)
                {
                    pictureGhost.Location = enemy.Location;
                    ghostHitPoint.Text = enemy.HitPoints.ToString();
                    pictureGhost.Visible = true;
                    if (enemy.HitPoints > 0)
                    {
                        enemiesShown++;
                    }
                    else
                    {
                        pictureGhost.Visible = false;
                    }
                }
                if (enemy is Ghoul)
                {
                    pictureGhoul.Location = enemy.Location;
                    ghoulHitPoint.Text = enemy.HitPoints.ToString();
                    pictureGhoul.Visible = true;
                    if (enemy.HitPoints > 0)
                    {
                        enemiesShown++;
                    }
                    else
                    {
                        pictureGhoul.Visible = false;
                    }
                }
            }   //End of Enemy foreach

            Control spellControl = null;
            switch (game.SpellInRoom.Name)
            {
                case "Arc":
                    spellControl = pictureArc; break;
                case "Bolt":
                    spellControl = pictureBolt; break;
                case "Sphere":
                    spellControl = pictureSphere; break;
                case "GreenPotion":
                    spellControl = pictureRed; break;
                case "BluePotion":
                    spellControl = pictureBlue; break;
                default: break;
            }
            spellControl.Visible = true;

            /* Inventory weapon PictureBoxes */
            if (game.CheckPlayerInventory("Arc"))
            { pictureEquipArc.Visible = true; }
            else
            { pictureEquipArc.Visible = false; }

            if (game.CheckPlayerInventory("Bolt"))
            {
                pictureEquipBolt.Visible = true;
            }
            else
            { pictureEquipBolt.Visible = false; }

            if (game.CheckPlayerInventory("Sphere"))
            { pictureEquipSphere.Visible = true; }
            else
            { pictureEquipSphere.Visible = false; }

            if (game.CheckPlayerInventory("GreenPotion"))
            { pictureEquipGreen.Visible = true; }
            else
            { pictureEquipGreen.Visible = false; }

            if (game.CheckPlayerInventory("BluePotion"))
            { pictureEquipBlue.Visible = true; }
            else
            { pictureEquipBlue.Visible = false; }

            /* Update Player, spell and level */
            spellControl.Location = game.SpellInRoom.Location;
            if (game.SpellInRoom.PickedUp)
            {
                spellControl.Visible = false;
                // To automatically load spell when no other
                if (game.Level == 1)
                {
                    game.Equip("Arc");
                    pictureEquipArc.BorderStyle = BorderStyle.FixedSingle;
                }
            }
            if (game.PlayerHitPoints <= 0)
            {
                picturePlayer.Visible = false;
                game.sounds.URL = @"audio\death.wav";
                game.sounds.controls.play();
                MessageBox.Show("You died");
                Application.Exit();
            }

            if (enemiesShown < 1)       // If made to this point and enemiesShown = 0 - all is dead
            {
                if (game.Level < 7)
                {
                    game.sounds.URL = @"audio\win.wav";
                    game.sounds.controls.play();
                    MessageBox.Show("You have defeated the enemies on this level");
                }

                batHitPoint.Text = "-";
                ghostHitPoint.Text = "-";
                ghoulHitPoint.Text = "-";
                game.NewLevel(random);
                round = 0;
                UpdateCharacters();
            }

        }
        // End of UpdateCharacter Method */

        /* Weapon Inventary */
        private void pictureEquipBolt_Click(object sender, EventArgs e)
        {
            game.Equip("Bolt");
            RemoveInventory();
            pictureEquipBolt.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(true);
        }

        private void pictureEquipArc_Click(object sender, EventArgs e)
        {
            game.Equip("Arc");
            RemoveInventory();
            pictureEquipArc.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(true);
        }

        private void pictureEquipSphere_Click(object sender, EventArgs e)
        {
            game.Equip("Sphere");
            RemoveInventory();
            pictureEquipSphere.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(true);
        }

        private void pictureEquipRed_Click(object sender, EventArgs e)
        {
            game.Equip("GreenPotion");
            RemoveInventory();
            pictureEquipGreen.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(false);
        }

        private void pictureEquipBlue_Click(object sender, EventArgs e)
        {
            game.Equip("BluePotion");
            RemoveInventory();
            pictureEquipBlue.BorderStyle = BorderStyle.FixedSingle;
            SetButtons(false);

        }

        private void SetButtons(bool action)
        {
            if (action)
            {
                buttonDrink.Visible = false;
                attackUp.Visible = true;
                attackRight.Visible = true;
                attackDown.Visible = true;
                attackLeft.Visible = true;
            }
            else
            {
                attackUp.Visible = false;
                attackRight.Visible = false;
                attackDown.Visible = false;
                attackLeft.Visible = false;
                buttonDrink.Visible = true;
            }
        }

        private void RemoveInventory()
        {
            pictureEquipBolt.BorderStyle = BorderStyle.None;
            pictureEquipArc.BorderStyle = BorderStyle.None;
            pictureEquipSphere.BorderStyle = BorderStyle.None;
            pictureEquipGreen.BorderStyle = BorderStyle.None;
            pictureEquipBlue.BorderStyle = BorderStyle.None;
        }

        /* Move Buttons */
        private void moveUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
        }

        private void moveLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
        }

        private void moveRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
        }

        /* Attack Buttons */
        private void attackUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
        }

        private void attackLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
        }

        private void attackRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
        }

        private void attackDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
        }

        private void buttonDrink_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);

            if (!game.PotionUsed("BluePotion"))
                pictureEquipBlue.Visible = false;
            if (!game.PotionUsed("GreenPotion"))
                pictureEquipGreen.Visible = false;
            UpdateCharacters();
        }

        // Method to move player with Arrow keys of keyboard
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                game.Move(Direction.Up, random);
                UpdateCharacters();
                return true;
            }
            if (keyData == Keys.Left)
            {
                game.Move(Direction.Left, random);
                UpdateCharacters();
                return true;
            }
            if (keyData == Keys.Right)
            {
                game.Move(Direction.Right, random);
                UpdateCharacters();
                return true;
            }
            if (keyData == Keys.Down)
            {
                game.Move(Direction.Down, random);
                UpdateCharacters();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
