
namespace Master_Jump.Abstractions
{
    public abstract class Physics
    {
        protected readonly Model Model;
        
        protected Physics(Model model)
        {
            Model = model;
        }

        public abstract bool CalculatePhysics(params object[] args);
        
        protected abstract bool CollisionCheck();
    }
    
}
