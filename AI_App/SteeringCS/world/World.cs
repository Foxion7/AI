using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;

namespace SteeringCS
{
    public class World
    {
        private Random _rnd = new Random();
        private List<MovingEntity> _goblins = new List<MovingEntity>();
        public List<Obstacle> Obstacles = new List<Obstacle>();
        public Vehicle Target { get; set; }
        public Vehicle Controlled { get; set; }
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
            Target = new Vehicle("Target", new Vector2D(600, 600), this);
            Target.VColor = Color.Green;
            Target.Pos = new Vector2D(100, 40);

            Controlled = new Vehicle("Target", new Vector2D(100,100), this);
            Controlled.VColor = Color.Blue;
            Controlled.Evader  = Target;
            Controlled.Pursuer = Target;
            Controlled.Target  = Target;

            SpawnObstacles();
        }

        public void SpawnObstacles()
        {
            Obstacle obstacle1 = new Obstacle("obstacle1", 20, new Vector2D(150, 150), this);
            Obstacles.Add(obstacle1);

            Obstacle obstacle2 = new Obstacle("obstacle2", 20, new Vector2D(400, 100), this);
            Obstacles.Add(obstacle2);

            Obstacle obstacle3 = new Obstacle("obstacle3", 20, new Vector2D(250, 300), this);
            Obstacles.Add(obstacle3);

            Obstacle obstacle4 = new Obstacle("obstacle4", 20, new Vector2D(600, 500), this);
            Obstacles.Add(obstacle4);

        }

        public void SpawnGoblins()
        {
            var dummy = new Goblin("dummy", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            dummy.SB = new SeekBehaviour(dummy);
            dummy.VColor = Color.Aqua;
            _goblins.Add(dummy);
            dummy.Evader = Target;
            dummy.Target = Target;

            //var purs = new Goblin("hunter", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            //purs.SB = new PursuitBehaviour<Goblin>(purs);
            //purs.VColor = Color.Crimson;
            //goblins.Add(purs);
            //purs.Evader = Target;
            //purs.Target = Target;


            //var gentleman = new Goblin("gentleman", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            //gentleman.SB = new PursuitAndArriveBehaviour<Goblin>(gentleman);
            //gentleman.VColor = Color.Purple;
            //goblins.Add(gentleman);
            //gentleman.Evader = Target;
            //gentleman.Target = Target;
        }

        public void DestroySeekers()
        {
            _goblins.Clear();
            
        }
        public void Update(float timeElapsed)
        {
            try
            {
                _goblins.ForEach(goblin =>
                {
                    goblin.Update(timeElapsed);
                    enforceNonPenetrationConstraint(goblin);
                });
                Controlled.Update(timeElapsed);
            }
            catch (Exception e)
            {
            }
        }

        public void Render(Graphics g)
        {
            _goblins.ForEach(e => e.Render(g));
            Obstacles.ForEach(e => e.Render(g));
            Target.Render(g);
            Controlled.Render(g);
        }

        public IEnumerable<MovingEntity> getGoblinNeighbors(Goblin goblin, double neighborsRange)
        {
            return _goblins.Where(g => !g.Equals(goblin));
        }

        public void Reset()
        {
            _goblins = new List<MovingEntity>();
            Obstacles = new List<Obstacle>();
            populate();
        }

        private void enforceNonPenetrationConstraint(BaseGameEntity current)
        {
            //zie pagina 125 van het boek. Mischien een idee om te implementeren
        }

        public IEnumerable<Obstacle> getObstacles()
        {
            return Obstacles;
        }
    }
}
