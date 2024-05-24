using System.Collections.Generic;
using System.Drawing;
using Master_Jump.Abstractions.Implementations;
using Master_Jump.Models;
using Master_Jump.Models.Interfaces;

namespace Master_Jump.Controllers
{
    public static class BulletController
    {
        public static readonly List<IBullet> Bullets = new List<IBullet>();
        
        private static void AddBullet(IBullet bullet)
        {
            Bullets.Add(bullet);
        }
        
        public static void GenerateBullet(Model model)
        {
            Model bulletModel = new Model(new PointF(model.Coordinates.X+15, model.Coordinates.Y), new Size(10,10));
            IBullet bullet = new Bullet(bulletModel, new BulletPhysics(bulletModel));
            Bullets.Add(bullet);
            bullet.BulletPhysics.CalculatePhysics();
        }
    }
}