﻿using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS
{
    public class World
    {
        private List<MovingEntity> seekers = new List<MovingEntity>();
        public Vehicle Target { get; set; }
        public Vehicle Player { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
        }

        private void populate()
        {
            Target = new Vehicle(new Vector2D(100, 60), this);
            Target.VColor = Color.Green;
            Target.Pos = new Vector2D(100, 40);

            Player = new Vehicle(new Vector2D(10, 10), this);
            Player.VColor = Color.Blue;
            Player.Target = Target;
            Player.Evader = Target;

        }

        public void SpawnSeekers()
        {
            var dummy = new Vehicle(new Vector2D(10, 10), this);
            dummy.SB = new SeekBehaviour<Vehicle>(dummy);
            dummy.VColor = Color.Aqua;
            seekers.Add(dummy);
            dummy.Evader = Player;
            dummy.Target = Player;

            var purs = new Vehicle(new Vector2D(10, 10), this);
            purs.SB = new PursuitBehaviour<Vehicle>(purs);
            purs.VColor = Color.Crimson;
            seekers.Add(purs);
            purs.Evader = Player;
            purs.Target = Player;


            var gentleman = new Vehicle(new Vector2D(10, 10), this);
            gentleman.SB = new PursuitAndArriveBehaviour<Vehicle>(gentleman);
            gentleman.VColor = Color.Purple;
            seekers.Add(gentleman);
            gentleman.Evader = Player;
            gentleman.Target = Player;
        }

        public void DestroySeekers()
        {
            seekers.Clear();
            
        }
        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in seekers)
            {
                me.Update(timeElapsed);
            }  
            Player.Update(timeElapsed);
        }

        public void Render(Graphics g)
        {
            seekers.ForEach(e => e.Render(g));
            Target.Render(g);
            Player.Render(g);
        }
    }
}
