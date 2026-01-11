namespace EggCentric.StateMachines
{
    public interface ITickableState
    {
        public void Tick(float timeStep);
    }
}