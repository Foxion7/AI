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
    class World
    {
        private List<MovingEntity> entities = new List<MovingEntity>();
        public Creature Target { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        private Random rnd = new Random();

        private int goblinAmount = 3;

        public World(int w, int h)
        {
            Width = w;
            Height = h;
            populate();
        }

        private void populate()
        {
            Target = new Creature(new Vector2D(100, 60), this);
            Target.VColor = Color.DarkRed;
            Target.Pos = new Vector2D(100, 40);
          
            // Adds enemies.
            AddGoblins();
        }

        private void AddGoblins()
        {
            for (int i = 0; i < this.goblinAmount; i++)
            {
                float startX = 10 + i * 10;
                float startY = 10 + i * 10;

                Creature goblin = new Creature(new Vector2D(startX, startY), this);

                // Gives random color. Should probably limit it to greens.
                goblin.VColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));

                entities.Add(goblin);
                goblin.Target = Target;
            }
        }

        public void Update(float timeElapsed)
        {
            foreach (MovingEntity me in entities)
            {
                me.Update(timeElapsed);
            }  
        }

        public void Render(Graphics g)
        {
            entities.ForEach(e => e.Render(g));
            Target.Render(g);
        }
    }
}
