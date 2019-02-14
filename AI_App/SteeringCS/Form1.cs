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
        }
        
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
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
                        pausedLabel.Visible = true;
                        timer.Interval = int.MaxValue;
                    }
                    else
                    {
                        paused = false;
                        pausedLabel.Visible = false;
                        timer.Interval = 20;
                    }
                    break;

            }
            if (keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right) {

                int manualMovementStrength = 100;
                Vector2D manualMovement = new Vector2D(world.Player.Pos.X, world.Player.Pos.Y);

                // Up arrow press.
                if (keyData == Keys.Up)
                {
                    if (world.Player.Pos.Y + manualMovementStrength >= 0)
                    {
                        manualMovement = new Vector2D(manualMovement.X, manualMovement.Y - manualMovementStrength);
                    }
                }

                // Down arrow press.
                if (keyData == Keys.Down)
                {
                    if (world.Player.Pos.Y - manualMovementStrength <= dbPanel1.Height)
                    {
                        manualMovement = new Vector2D(manualMovement.X, manualMovement.Y + manualMovementStrength);
                    }
                }

                // Left arrow press.
                if (keyData == Keys.Left)
                {
                    if (world.Player.Pos.X - manualMovementStrength >= 0)
                    {
                        manualMovement = new Vector2D(manualMovement.X - manualMovementStrength, manualMovement.Y);
                    }
                }

                // Right arrow press.
                if (keyData == Keys.Right) { 
                    if (world.Player.Pos.X + manualMovementStrength <= dbPanel1.Width)
                    {
                        manualMovement = new Vector2D(manualMovement.X + manualMovementStrength, manualMovement.Y);
                    }
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
    }
}
