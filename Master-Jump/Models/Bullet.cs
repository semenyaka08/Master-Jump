using System.Drawing;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Models.Interfaces;
using Master_Jump.Properties;

namespace Master_Jump.Models
{
    public class Bullet : IBullet
    {
        
        public bool IsTouched { get; set; }

        public Image Sprite { get; set; }
        public Model Model { get; set; }
        
        public BulletPhysics BulletPhysics { get; set; }

        public Bullet(Model model, BulletPhysics bulletPhysics)
        {
            IsTouched = false;
            
            Sprite = Resources.bullet;
            
            Model = model;

            BulletPhysics = bulletPhysics;
        }
        
        public void DrawBullet(Graphics graphics)
        {
            graphics.DrawImage(Sprite, Model.Coordinates.X, Model.Coordinates.Y, Model.Size.Width, Model.Size.Height);
        }
    }
}