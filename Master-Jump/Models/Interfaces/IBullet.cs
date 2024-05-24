using System.Drawing;
using Master_Jump.Abstractions.Implementations;

namespace Master_Jump.Models.Interfaces
{
    public interface IBullet
    {
        Image Sprite { get; set; }

        Model Model { get; set; }
        
        BulletPhysics BulletPhysics { get; set; }
        
        void DrawBullet(Graphics graphics);
    }
}