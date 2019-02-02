using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringCS.Interfaces;
using SteeringCS.util;

namespace SteeringCS
{
    public class World
    {
        private Random _rnd = new Random();
        private List<MovingEntity> _goblins;
        private CellSpacePartition<MovingEntity> _goblinSpace;
        private List<MovingEntity> _hobgoblins = new List<MovingEntity>();
        public List<IObstacle> Obstacles = new List<IObstacle>();
        public Creature Target { get; set; }
        //public Creature Controlled { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Color> goblinColors { get; }
        public bool TriangleModeActive { get; set; }
        public bool VelocityVisible { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            TriangleModeActive = false;
            VelocityVisible = true;

            _goblinSpace = new CellSpacePartition<MovingEntity>(w,h,20,20);
            _goblins = new List<MovingEntity>();
            goblinColors = new List<Color>();
            goblinColors.Add(Color.Green);
            goblinColors.Add(Color.ForestGreen);
            goblinColors.Add(Color.DarkOliveGreen);
            goblinColors.Add(Color.DarkGreen);

            populate();
        }

        private void populate()
        {
            Target = new Creature("Player", new Vector2D(600, 600), this);
            Target.VColor = Color.Blue;
            Target.Pos = new Vector2D(500, 300);
            
            SpawnObstacles();
        }

        public void SpawnObstacles()
        {
            Obstacle obstacle1 = new Obstacle("obstacle1", 20, new Vector2D(150, 150), this);
            Obstacles.Add(obstacle1);

            Obstacle obstacle2 = new Obstacle("obstacle2", 40, new Vector2D(400, 100), this);
            Obstacles.Add(obstacle2);

            Obstacle obstacle3 = new Obstacle("obstacle3", 20, new Vector2D(250, 300), this);
            Obstacles.Add(obstacle3);

            Obstacle obstacle4 = new Obstacle("obstacle4", 20, new Vector2D(600, 500), this);
            Obstacles.Add(obstacle4);

        }

        public void SpawnGoblins()
        {
            Random r = new Random();
            int rInt = r.Next(0, goblinColors.Count());

            //var dummy = new Goblin("dummy", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            //dummy.SB = new ArrivalBehaviour(dummy);
            //dummy.VColor = goblinColors[rInt];
            //_goblins.Add(dummy);
            //dummy.Evader = Target;
            //dummy.Target = Target;

            //var purs = new Goblin("hunter", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            //purs.SB = new PursuitBehaviour(purs);
            //purs.VColor = goblinColors[rInt];
            //_goblins.Add(purs);
            //purs.Evader = Target;
            //purs.Target = Target;


            var gentleman = new Goblin("gentleman", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            gentleman.VColor = goblinColors[rInt];
            gentleman.Target = Target;
            _goblins.Add(gentleman);
        }

        public void SpawnHobgoblin()
        {
            var hobbyGobby = new Hobgoblin("Bloodbeard", new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this);
            hobbyGobby.PB = new SeekBehaviour(hobbyGobby);
            hobbyGobby.VColor = Color.Black;
            _hobgoblins.Add(hobbyGobby);
            hobbyGobby.Evader = Target;
            hobbyGobby.Target = Target;
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
            }
            catch (Exception e)
            {
                Console.WriteLine("Goblin exception: " + e.Message + " stacktrace: " + e.StackTrace);
            }
            try
            {
                _hobgoblins.ForEach(hobgoblin =>
                {
                    hobgoblin.Update(timeElapsed);
                    //enforceNonPenetrationConstraint(hobgoblin);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Hobgoblin exception: " + e.Message + " stacktrace: " +  e.StackTrace);
            }
            Target.Update(timeElapsed);
        }

        public void Render(Graphics g)
        {
            _goblins.ForEach(e => e.Render(g));
            _hobgoblins.ForEach(e => e.Render(g));
            Obstacles.ForEach(e => e.Render(g));
            Target.Render(g);
        }

        public IEnumerable<MovingEntity> GetGoblinNeighbors(Goblin goblin, double neighborsRange)
        {
            return _goblinSpace.CalculateNeighbors(goblin.Pos, neighborsRange);
        }

        public void Reset()
        {
            _goblins = new List<MovingEntity>();
            _goblinSpace.EmptyCells();
            _hobgoblins = new List<MovingEntity>();
            Obstacles = new List<IObstacle>();
            populate();
        }

        private void enforceNonPenetrationConstraint(BaseGameEntity current)
        {
            //zie pagina 125 van het boek. Mischien een idee om te implementeren
        }

        public List<IObstacle> getObstacles()
        {
            return Obstacles;
        }
        
        public List<MovingEntity> getHobgoblins()
        {
            return _hobgoblins;
        }

        public void ReposGoblin(Goblin g, Vector2D oldPos)
        {
            _goblinSpace.UpdateEntity(g, oldPos);
        }
    }
}
