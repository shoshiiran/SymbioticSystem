namespace Symbiotic.Actor
{
    public interface IUpdateInterface
    {
        void PreUpdate();
        void OnUpdate();
        void PostUpdate();
    }
}