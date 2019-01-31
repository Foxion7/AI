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

namespace SteeringCS
{
    public partial class Form1 : Form
    {
        World world;
        System.Timers.Timer timer;

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
                world.Player.Pos = new Vector2D(e.X, e.Y);
            }
            else
            {
                world.Target.Pos = new Vector2D(e.X, e.Y);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    world.Target.SB = new SeekBehaviour<Vehicle>(world.Target);
                    break;
                case Keys.X:
                    world.Target.SB = new FleeBehaviour<Vehicle>(world.Target);
                    break;
                case Keys.C:
                    world.Target.SB = new ArrivalBehaviour<Vehicle>(world.Target);
                    break;
                case Keys.V:
                    world.Target.SB = new PursuitBehaviour<Vehicle>(world.Target);
                    break;
                case Keys.B:
                    world.Target.SB = new PursuitAndArriveBehaviour<Vehicle>(world.Target);
                    break;
                case Keys.N:
                    //world.Target2.SB = new WanderBehaviour<Vehicle>(world.Target2);
                    break;
            }
        }
    }
}
