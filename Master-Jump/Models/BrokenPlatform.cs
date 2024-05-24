using System.Drawing;
using Master_Jump.Models.Interfaces;
using Master_Jump.Properties;

namespace Master_Jump.Models
{
    public class BrokenPlatform : IPlatform
    {
        public BrokenPlatform(Point coordinates)
        {
            Sprite = Resources.brokenplatform;
            Model = new Model(coordinates, new Size(75,15));
        }
        
        public Image Sprite { get; set; }
        public Model Model { get; set; }
        
        public bool IsTouched { get; set; }
        
        public void DrawPlatform(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y, Model.Size.Width, Model.Size.Height);
        }
    }
}