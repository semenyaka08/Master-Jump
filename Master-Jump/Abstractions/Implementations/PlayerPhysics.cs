using System.Linq;
using System.Threading;
using Master_Jump.Controllers;
using Master_Jump.Models;
using Master_Jump.Properties;

namespace Master_Jump.Abstractions.Implementations
{
    public class PlayerPhysics : Physics
    {
        public int XCoords { get; set; }
        
        private float Gravity { get; set; }
        
        private float Force { get; set; }
        
        public PlayerPhysics(Model model) : base(model)
        {
            Gravity = 0;
            Force = 0.35f;
        }

        public override bool CalculatePhysics(params object[] args) //It should get zero parameters
        {
            Model.Coordinates.X += XCoords;
            
            if (Model.Coordinates.Y < 700)
            {
                Model.Coordinates.Y += Gravity;
                Gravity += Force;
                return CollisionCheck();
            }

            return false;        
        }

        protected override bool CollisionCheck()
        {
            if (EnemyClash())
                return true;
            foreach (var platform in from platform in PlatformController.Platforms where Model.Coordinates.X + Model.Size.Width / 2 + 10 >= platform.Model.Coordinates.X && Model.Coordinates.X + Model.Size.Width/2 - 10 <= platform.Model.Coordinates.X + platform.Model.Size.Width where Model.Coordinates.Y + Model.Size.Height >= platform.Model.Coordinates.Y && Model.Coordinates.Y + Model.Size.Height <= platform.Model.Coordinates.Y + platform.Model.Size.Height where Gravity > 0 select platform)
            {
                if (!(platform is BrokenPlatform))
                {
                    Gravity = -10;
                    if (platform.IsTouched) continue;
                    PlatformController.GeneratePlatforms();
                    platform.IsTouched = true;
                    return platform.IsTouched;
                }
                
                platform.Sprite = Resources.brokenplatform2;
                platform.IsTouched = true;
                
                ThreadStart collector = Form1.MoveBrokenPlatforms;
                Thread moving = new Thread(collector);
                moving.Start();
            }

            return false;
        }

        private bool EnemyClash()
        {
            foreach (var enemy in EnemyController.Enemies)
            {
                bool collisionX = Model.Coordinates.X < enemy.Model.Coordinates.X + enemy.Model.Size.Width - 15 &&
                                  Model.Coordinates.X + Model.Size.Width > enemy.Model.Coordinates.X + 5;
                
                bool collisionY = Model.Coordinates.Y < enemy.Model.Coordinates.Y + enemy.Model.Size.Height - 15 &&
                                  Model.Coordinates.Y + Model.Size.Height > enemy.Model.Coordinates.Y;
                
                if (collisionX && collisionY)
                {
                    Form1.Player.IsFalling = true;
                    return Form1.Player.IsFalling;
                }
            }

            return false;
        }
    }
}