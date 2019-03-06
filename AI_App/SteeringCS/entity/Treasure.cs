using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringCS.entity
{
    public class Treasure : BaseGameEntity
    {
        public double value { get; set; }
        public double size { get; set; }
        public Image image { get; set; }

        public Treasure(string name, double value, Vector2D pos, World w, double size) : base(name, pos, w)
        {
            this.value = value;
            this.size = size;

            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            image = Image.FromFile(@projectDirectory + "/images/treasure.png");
        }

        public override void Update(float delta)
        {

        }

        public override void Render(Graphics g)
        {
            g.DrawImage(image, (float)(Pos.X - size / 2), (float)(Pos.Y - size / 2), (float)(size), (float)(size));
        }
    }
}
