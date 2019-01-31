using SteeringCS.behaviour;
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
        public List<Obstacle> obstacles = new List<Obstacle>();
        public Vehicle Player { get; set; }
        public Vehicle Target { get; set; }
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
            Player = new Vehicle("Player", new Vector2D(100, 60), this);
            Player.VColor = Color.Green;
            Player.Pos = new Vector2D(100, 40);

            Target = new Vehicle("Gobbo1", new Vector2D(10, 10), this);
            Target.VColor = Color.Blue;
            Target.Target = Player;
            Target.Evader = Player;

            SpawnObstacles();
        }

        public void SpawnObstacles()
        {
            Obstacle obstacle1 = new Obstacle("obstacle1", 50, new Vector2D(150, 150), this);
            obstacles.Add(obstacle1);

            Obstacle obstacle2 = new Obstacle("obstacle2", 20, new Vector2D(400, 100), this);
            obstacles.Add(obstacle2);

            Obstacle obstacle3 = new Obstacle("obstacle3", 50, new Vector2D(250, 300), this);
            obstacles.Add(obstacle3);
        }

        public void SpawnSeekers()
        {
            var dummy = new Vehicle("Gobbo2", new Vector2D(10, 10), this);
            dummy.SB = new SeekBehaviour<Vehicle>(dummy);
            dummy.VColor = Color.Aqua;
            seekers.Add(dummy);
            dummy.Evader = Target;
            dummy.Target = Target;

            var purs = new Vehicle("Gobbo3", new Vector2D(10, 10), this);
            purs.SB = new PursuitBehaviour<Vehicle>(purs);
            purs.VColor = Color.Crimson;
            seekers.Add(purs);
            purs.Evader = Target;
            purs.Target = Target;


            var gentleman = new Vehicle("Gobbo4", new Vector2D(10, 10), this);
            gentleman.SB = new PursuitAndArriveBehaviour<Vehicle>(gentleman);
            gentleman.VColor = Color.Purple;
            seekers.Add(gentleman);
            gentleman.Evader = Target;
            gentleman.Target = Target;
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
            obstacles.ForEach(e => e.Render(g));
            Player.Render(g);
            Target.Render(g);
        }
    }
}
