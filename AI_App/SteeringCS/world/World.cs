using SteeringCS.behaviour;
using SteeringCS.entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using SteeringCS.Interfaces;
using SteeringCS.util;
using SteeringCS.util.Graph;
using SteeringCS._goals;

namespace SteeringCS
{
    public class World
    {
        private Random _rnd = new Random();
        private VectorGraph Graph { get; set; }

        private List<MovingEntity> _goblins;
        private CellSpacePartition<MovingEntity> _goblinSpace;
        private List<MovingEntity> _hobgoblins = new List<MovingEntity>();
        private List<Corpse> _corpses = new List<Corpse>();
        private List<Treasure> _treasures = new List<Treasure>();
        private List<IObstacle> _obstacles = new List<IObstacle>();
        private List<IWall> _walls = new List<IWall>();

        public Hero Hero { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Color> goblinColors { get; }
        private int goblinCount = 0;
        private int hobgoblinCount = 0;
        public bool DebugMode { get; set; }
        public bool TriangleModeActive { get; set; }
        public bool VelocityVisible { get; set; }
        public bool GraphVisible { get; set; }

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            TriangleModeActive = false;
            VelocityVisible = false;
            DebugMode = true;

            _goblinSpace = new CellSpacePartition<MovingEntity>(w,h,5,5);
            _goblins = new List<MovingEntity>();
            goblinColors = new List<Color>();
            goblinColors.Add(Color.Green);
            goblinColors.Add(Color.ForestGreen);
            goblinColors.Add(Color.DarkOliveGreen);
            goblinColors.Add(Color.DarkGreen);

            SpawnObstacles();
            SpawnWalls();
            GenerateTreasure(10, 30, new Vector2D(100, 500));

            Graph = new VectorGraph(w, h, 75, _obstacles, _walls);
            populate();
        }

        private void populate()
        {
            Hero = new Hero("Player", new Vector2D(600, 600), this);
            Hero.VColor = Color.Blue;
            Hero.Pos = new Vector2D(500, 300);
        }

        public void SpawnObstacles()
        {
            Obstacle obstacle1 = new Obstacle("obstacle1", 20, new Vector2D(600, 550), this);
            _obstacles.Add(obstacle1);

            Obstacle obstacle2 = new Obstacle("obstacle2", 20, new Vector2D(800, 550), this);
            _obstacles.Add(obstacle2);

            Obstacle obstacle3 = new Obstacle("obstacle3", 20, new Vector2D(600, 450), this);
            _obstacles.Add(obstacle3);

            Obstacle obstacle4 = new Obstacle("obstacle4", 20, new Vector2D(800, 450), this);
            _obstacles.Add(obstacle4);
        }


        public void SpawnWalls()
        {
            Wall wall1 = new Wall("wall1", 100, 20, new Vector2D(600, 350), this);
            _walls.Add(wall1);

            Wall wall2 = new Wall("wall2", 100, 20, new Vector2D(740, 350), this);
            _walls.Add(wall2);

            Wall wall3 = new Wall("wall3", 20, 200, new Vector2D(600, 350 - 200), this);
            _walls.Add(wall3);

            Wall wall4 = new Wall("wall4", 20, 200, new Vector2D(820, 350 - 200), this);
            _walls.Add(wall4);

            Wall wall5 = new Wall("wall5", 240, 20, new Vector2D(600, 350 - 220), this);
            _walls.Add(wall5);

            Wall wall6 = new Wall("wall6", 30, 300, new Vector2D(300, 325), this);
            _walls.Add(wall6);
        }

        public void SpawnGoblin()
        {
            Random r = new Random();
            int rInt = r.Next(0, goblinColors.Count());
            goblinCount++;
            
            var goblin = new Goblin("Goblin" + goblinCount.ToString(), new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this, Hero);
            goblin.VColor = goblinColors[rInt];
            _goblins.Add(goblin);
            _goblinSpace.Add(goblin.Key, goblin);
        }

        public void SpawnHobgoblin()
        {
            hobgoblinCount++;

            var hobgoblin = new Hobgoblin("Hobgoblin" + hobgoblinCount.ToString(), new Vector2D(_rnd.Next(0, Width), _rnd.Next(0, Height)), this, Hero);
            hobgoblin.VColor = Color.Black;
            _hobgoblins.Add(hobgoblin);
            hobgoblin.Evader = Hero;
            hobgoblin.Target = Hero;
        }

        public void SpawnCorpse(double size, Vector2D pos)
        {
            var corpse = new Corpse("Dead guy", pos, this, size);
            _corpses.Add(corpse);
        }

        public void GenerateTreasure(double value, double size, Vector2D pos)
        {
            var treasure = new Treasure("Treasure", value, pos, this, size);
            _treasures.Add(treasure);
        }

        public void DestroyGoblin(Goblin goblin)
        {
            _goblins.Remove(goblin);
        }

        public void DestroyTreasure(Treasure treasure)
        {
            _treasures.Remove(treasure);
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
                    enforceNonPenetrationConstraint(hobgoblin);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Hobgoblin exception: " + e.Message + " stacktrace: " +  e.StackTrace);
            }

            try
            {
                _corpses.ForEach(corpse =>
                {
                    corpse.Update(timeElapsed);
                });
            }
            catch (Exception e)
            {
                Console.WriteLine("Corpse exception: " + e.Message + " stacktrace: " + e.StackTrace);
            }

            Hero.Update(timeElapsed);
        }

        public void Render(Graphics g)
        {
            if(GraphVisible)
                Graph.Render(g);
            _corpses.ForEach(e => e.Render(g));
            _treasures.ForEach(e => e.Render(g));
            _goblins.ToList().ForEach(e => e.Render(g));
            _hobgoblins.ForEach(e => e.Render(g));
            _obstacles.ForEach(e => e.Render(g));
            _walls.ForEach(e => e.Render(g));
            Hero.Render(g);
        }

        public IEnumerable<MovingEntity> GetGoblinNeighbors(Goblin goblin, double neighborsRange)
        {
            return _goblinSpace.CalculateNeighbors(goblin.Pos, neighborsRange).ToList();
        }

        public void Reset()
        {
            _goblins = new List<MovingEntity>();
            _goblinSpace.EmptyCells();
            _hobgoblins = new List<MovingEntity>();
            _corpses = new List<Corpse>();
            _obstacles = new List<IObstacle>();
            _walls = new List<IWall>();
            SpawnObstacles();
            SpawnWalls();
            populate();

            Hero.currentGoal = new Think("Think", Hero);
            Hero.currentGoal.Enter();
        }

        public void rePosGoblin(int key, Vector2D oldPos, Vector2D newPos)
        {
            _goblinSpace.UpdateEntity(key, oldPos, newPos);
        }

        private void enforceNonPenetrationConstraint(BaseGameEntity current)
        {
            //zie pagina 125 van het boek. Mischien een idee om te implementeren
        }

        public List<IObstacle> getObstacles()
        {
            return _obstacles;
        }

        public List<IWall> getWalls()
        {
            return _walls;
        }

        public List<Treasure> getTreasure()
        {
            return _treasures;
        }
        
        public List<MovingEntity> getHobgoblins()
        {
            return _hobgoblins;
        }
        
        
        public List<MovingEntity> getGoblins()
        {
            return _goblins;
        }

        public Route getRoute(Vector2D start, Vector2D end)
            => new Route(PathFinding.AStar(Graph, start, end, PathFinding.Manhatten).PathSmoothing(_walls, _obstacles).ToList());
        

        public void setPlayerRoute(Vector2D end)
        {
            if (Hero == null)
                return;
            Hero.Path = new Route(PathFinding.AStar(Graph, Hero.Pos, end, PathFinding.Manhatten).PathSmoothing(_walls, _obstacles).ToList());
        }

        public Vector2D RandomPos()=> Graph.Nodes.ElementAt(_rnd.Next(Graph.Nodes.Count())).Data;
        
    }
}
