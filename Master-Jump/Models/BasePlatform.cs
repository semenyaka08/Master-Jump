using System.Drawing;
using Master_Jump.Models.Interfaces;

namespace Master_Jump.Models
{
    public class BasePlatform : IPlatform
    {
        public Image Sprite { get; set; }

        public Model Model { get; set; }

        public bool IsTouched { get; set; }
        
        public BasePlatform(Point coordinates)
        {
            Sprite = Properties.Resources.platform;
            Model = new Model(coordinates, new Size(60,12));
        }
        
        public void DrawPlatform(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y);
        }
    }
}