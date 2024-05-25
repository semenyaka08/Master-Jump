using System.Threading;
using System.Threading.Tasks;
using Master_Jump.Controllers;

namespace Master_Jump.Abstractions.Implementations
{
    public class BulletPhysics : Physics
    {
        public BulletPhysics(Model model) : base(model)
        {
            
        }

        public override bool CalculatePhysics()
        {
            while (Model.Coordinates.Y > 50)
            {
                Model.Coordinates.Y -= 10;
                Thread.Sleep(10);
            }
            BulletController.ClearBullet();
            return true;
        }

        protected override bool CollisionCheck()
        {
            throw new System.NotImplementedException();
        }
    }
}