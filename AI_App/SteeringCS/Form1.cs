using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.behaviour;
using SteeringCS.entity;
using SteeringCS.Interfaces;
using SteeringCS.util;

namespace SteeringCS
{
    public partial class Form1 : Form
    {
        World world;
        System.Timers.Timer timer;
        bool paused = false;

        public const float timeDelta = 0.8f;
        
        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
            
            // Starting values goblin.
            forceSpinnerGoblin.Value = 25;
            massSpinnerGoblin.Value = 50;
            maxSpeedSpinnerGoblin.Value = 5;

            // Starting values hobgoblin.
            forceSpinnerHobgoblin.Value = 40;
            massSpinnerHobgoblin.Value = 100;
            maxSpeedSpinnerHobgoblin.Value = 5;
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            ActiveControl = dbPanel1;
            //ModifyProgressBarColor.SetState(health, 2);
            //ModifyProgressBarColor.SetState(cooldown, 1);
            //ModifyProgressBarColor.SetState(stamina, 1);

            if(world.Hero != null)
            {
                //healthBar = new util.StatusBar(15, 37, 50, 10);
                healthBar.Value = world.Hero.maxHealth;
                ////healthBar.Location = new Point(15, 37);
                //HeroPanel.Controls.Add(healthBar);

                //staminaBar = new util.StatusBar(14, 103, 50, 10);
                staminaBar.Value = world.Hero.maxStamina;
                ////staminaBar.Location = new Point(14, 103);
                //HeroPanel.Controls.Add(staminaBar);

                //cooldownBar = new util.StatusBar(14, 169, 50, 10);
                cooldownBar.Value = world.Hero.maxCooldown;
                ////cooldownBar.Location = new Point(14, 169);
                //HeroPanel.Controls.Add(cooldownBar);
            }

        }
        
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            CalculateStats();
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }

        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                world.setPlayerRoute(new Vector2D(e.X, e.Y));
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.E:
                    world.Hero.Attack();
                    break;
                case Keys.T:
                    world.TriangleModeActive = !world.TriangleModeActive;
                    break;
                case Keys.V:
                    world.VelocityVisible = !world.VelocityVisible;
                    break;
                case Keys.G:
                    world.SpawnGoblins();
                    break;
                case Keys.H:
                    world.SpawnHobgoblin();
                    break;
                case Keys.R:
                    world.Reset();
                    break;
                case Keys.P:
                    world.GraphVisible = !world.GraphVisible;
                    break;
                case Keys.Space:
                    if (!paused)
                    {
                        paused = true;
                        pausedLabel.Text = "Paused";
                        timer.Interval = int.MaxValue;
                    }
                    else
                    {
                        paused = false;
                        pausedLabel.Text = "Playing";
                        timer.Interval = 20;
                    }
                    break;
                case Keys.Escape:
                    Environment.Exit(0);
                    break;

            }

            // Arrow movement
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right) {

                int manualMovementStrength = 100;
                Vector2D manualMovement = new Vector2D(world.Hero.Pos.X, world.Hero.Pos.Y);

                // Up arrow press.
                if (keyData == Keys.Up)
                {
                    manualMovement = new Vector2D(manualMovement.X, manualMovement.Y - manualMovementStrength);
                }

                // Down arrow press.
                if (keyData == Keys.Down)
                {
                    manualMovement = new Vector2D(manualMovement.X, manualMovement.Y + manualMovementStrength);
                }

                // Left arrow press.
                if (keyData == Keys.Left)
                {
                    manualMovement = new Vector2D(manualMovement.X - manualMovementStrength, manualMovement.Y);
                }

                // Right arrow press.
                if (keyData == Keys.Right) {
                    manualMovement = new Vector2D(manualMovement.X + manualMovementStrength, manualMovement.Y);
                }
                world.setPlayerRoute(manualMovement);
            }
            return true;
        }

        private void forceSpinnerGoblin_ValueChanged(object sender, EventArgs e)
        {
            foreach (Goblin goblin in world.getGoblins())
            {
                goblin.MaxForce = (float)forceSpinnerGoblin.Value;
            }
        }

        private void massSpinnerGoblin_ValueChanged(object sender, EventArgs e)
        {
            foreach (Goblin goblin in world.getGoblins())
            {
                goblin.Mass = (float)massSpinnerGoblin.Value;
            }
        }

        private void maxSpeedSpinnerGoblin_ValueChanged(object sender, EventArgs e)
        {
            foreach (Goblin goblin in world.getGoblins())
            {
                goblin.MaxSpeed = (float)maxSpeedSpinnerGoblin.Value;
            }
        }

        private void forceSpinnerHobgoblin_ValueChanged(object sender, EventArgs e)
        {
            foreach (Hobgoblin hobgoblin in world.getHobgoblins())
            {
                hobgoblin.MaxForce = (float)forceSpinnerHobgoblin.Value;
            }
        }

        private void massSpinnerHobgoblin_ValueChanged(object sender, EventArgs e)
        {
            foreach (Hobgoblin hobgoblin in world.getHobgoblins())
            {
                hobgoblin.MaxForce = (float)massSpinnerHobgoblin.Value;
            }
        }

        private void maxSpeedSpinnerHobgoblin_ValueChanged(object sender, EventArgs e)
        {
            foreach (Hobgoblin hobgoblin in world.getHobgoblins())
            {
                hobgoblin.MaxForce = (float)maxSpeedSpinnerHobgoblin.Value;
            }
        }

        private void CalculateStats()
        {
            if (world.Hero != null)
            {
                int incrementStamina = 1;
                int incrementCooldown = 1;

                world.Hero.RecoverStamina(incrementStamina);
                world.Hero.RecoverCooldown(incrementCooldown);

                healthBar.Value = world.Hero.health;
                staminaBar.Value = world.Hero.stamina;
                cooldownBar.Value = world.Hero.cooldown;
                UpdateStats();
            }
        }

        private void UpdateStats()
        {
            healthBar.Refresh();
            staminaBar.Refresh();
            cooldownBar.Refresh();
        }
    }
}
