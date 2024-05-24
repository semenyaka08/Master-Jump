namespace Master_Jump.Abstractions.Implementations
{
    public class BulletPhysics : Physics
    {
        public BulletPhysics(Model model) : base(model)
        {
            
        }

        public override bool CalculatePhysics()
        {
            throw new System.NotImplementedException();
        }

        protected override bool CollisionCheck()
        {
            throw new System.NotImplementedException();
        }
    }
}