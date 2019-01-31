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
                world.Target.Pos = new Vector2D(e.X, e.Y);
            }
            else
            {
                world.Player.Pos = new Vector2D(e.X, e.Y);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Z:
                    world.Player.SB = new SeekBehaviour<Vehicle>(world.Player);
                    break;
                case Keys.X:
                    world.Player.SB = new FleeBehaviour<Vehicle>(world.Player);
                    break;
                case Keys.C:
                    world.Player.SB = new ArrivalBehaviour<Vehicle>(world.Player);
                    break;
                case Keys.V:
                    world.Player.SB = new PursuitBehaviour<Vehicle>(world.Player);
                    break;
                case Keys.B:
                    world.Player.SB = new PursuitAndArriveBehaviour<Vehicle>(world.Player);
                    break;
                case Keys.N:
                    //world.Player.SB = new WanderBehaviour<Vehicle>(world.Player);
                    break;
            }
        }
    }
}
