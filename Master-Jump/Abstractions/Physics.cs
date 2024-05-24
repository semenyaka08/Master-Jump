namespace Master_Jump.Abstractions
{
    public abstract class Physics
    {
        protected readonly Model Model;
        
        protected Physics(Model model)
        {
            Model = model;
        }

        public abstract bool CalculatePhysics();

        protected abstract bool CollisionCheck();
    }
    
}
