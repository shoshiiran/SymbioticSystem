namespace Symbiotic.Actor
{
    public interface ISystem { }
    public interface IInitInterface : ISystem
    {
        void OnInit(IWorldSystem system);
    }
}