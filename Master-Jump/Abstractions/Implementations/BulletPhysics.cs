using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using Master_Jump.Controllers;
using Master_Jump.Models;
using Master_Jump.Models.Interfaces;


namespace Master_Jump.Abstractions.Implementations
{
    public class BulletPhysics : Physics
    {
        public BulletPhysics(Model model) : base(model)
        {
            
        }

        public override bool CalculatePhysics(params object[] args)   //It should get 1 parameter, PointF 
        {
            if (!(args[0] is PointF end))
            {
                return false;
            }
            CalculateSplitPoint(ref Model.Coordinates, ref end);
            return true;
        }

        private void CalculateSplitPoint(ref PointF start, ref PointF end)
        {
            double distance = Math.Sqrt(Math.Pow(end.X - start.X, 2) + Math.Pow(end.Y - start.Y, 2));
            const int step = 10;
            
            double abx = end.X - start.X;
            double aby = end.Y - start.Y;
            
            double stepX = (abx / distance) * step;
            double stepY = (aby / distance) * step;
            
            while (start.X >= 0 && end.X <= 330)
            {
                start.X += (float)stepX;
                start.Y += (float)stepY;
                CollisionCheck();
                Thread.Sleep(10);
            }
        }
        
        protected override bool CollisionCheck()
        {
            foreach (var enemy in EnemyController.Enemies.Where(enemy => Model.Coordinates.X >= enemy.Model.Coordinates.X &&
                                                                         Model.Coordinates.X <= enemy.Model.Coordinates.X + enemy.Model.Size.Width &&
                                                                         Model.Coordinates.Y >= enemy.Model.Coordinates.Y &&
                                                                         Model.Coordinates.Y <= enemy.Model.Coordinates.Y + enemy.Model.Size.Height))
            {
                IBullet bullet = BulletController.Bullets.FirstOrDefault(p => p.Model == Model);

                if (bullet!= null && enemy is Enemy enemy1 && bullet is Bullet bullet1)
                {
                    enemy1.IsTouched = true;
                    bullet1.IsTouched = true;
                }
            }

            return false;
        }
    }
}