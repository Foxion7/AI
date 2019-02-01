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
        ISteeringBehaviour<IMover> behaviour;

        public const float timeDelta = 0.8f;
        
        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 20;
            timer.Enabled = true;
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
                world.Target.Pos = new Vector2D(e.X, e.Y);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    world.Target.SB = new SeekBehaviour(world.Target);
                    break;
                case Keys.X:
                    world.Target.SB = new FleeBehaviour(world.Target);
                    break;
                case Keys.C:
                    world.Target.SB = new ArrivalBehaviour(world.Target);
                    break;
                case Keys.V:
                    world.Target.SB = new PursuitAndArriveBehaviour(world.Target);
                    break;
                case Keys.B:
                    world.Target.SB = new PursuitAndArriveBehaviour<Creature>(world.Target);
                    break;
                case Keys.N:
                    world.Target.SB = new WanderBehaviour(world.Target);
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
        }

        //public void SetGoblinBehaviour(ISteeringBehaviour<IMover> behaviour)
        //{
        //    foreach (Goblin goblin in world.getGoblins())
        //    {
        //        goblin.UB = behaviour;
        //        goblin.UB.ME = goblin;
        //    }
        //}

        //public void SetGoblinBehaviour(ISteeringBehaviour<Goblin> behaviour)
        //{
        //    foreach (Goblin goblin in world.getGoblins())
        //    {
        //        goblin.UB = behaviour;
        //        goblin.UB.ME = goblin;
        //    }
        //}
    }
}
