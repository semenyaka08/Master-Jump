using System.Drawing;
using Master_Jump.Abstractions;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Models.Interfaces;
using Master_Jump.Properties;

namespace Master_Jump.Models
{
    public class Player : IBaseUnit
    {
        public Image Sprite { get; set; }
        
        public bool IsFalling { get; set; }
        
        public Physics Physics { get; set; }

        public Model Model { get; set; }

        public Player(Model model, PlayerPhysics physics)
        {
            Sprite = Resources.man;
            
            Model = model;

            Physics = physics;
        }
        
        public void DrawUnit(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y, Model.Size.Width, Model.Size.Height);
        }
    }
}