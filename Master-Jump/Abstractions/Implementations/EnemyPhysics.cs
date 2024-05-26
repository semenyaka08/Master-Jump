namespace Master_Jump.Abstractions.Implementations
{
    public class EnemyPhysics : Physics
    {
        public EnemyPhysics(Model model) : base(model)
        {
        }

        public override bool CalculatePhysics(params object[] args)
        {
            throw new System.NotImplementedException();
        }

        protected override bool CollisionCheck()
        {
            throw new System.NotImplementedException();
        }
    }
}