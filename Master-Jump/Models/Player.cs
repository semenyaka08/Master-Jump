using System.Drawing;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Models.Interfaces;

namespace Master_Jump.Models
{
    public class Player : IBaseUnit
    {
        public Image Sprite { get; set; }
        
        public bool IsFalling { get; set; }
        
        public PlayerPhysics Physics { get; set; }

        public Model Model { get; set; }

        public Player()
        {
            Sprite = Properties.Resources.man;
            
            Model = new Model(new Point(100, 350), new Size(40,40));

            Physics = new PlayerPhysics(Model);
        }
        
        public void DrawUnit(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y, Model.Size.Width, Model.Size.Height);
        }
    }
}