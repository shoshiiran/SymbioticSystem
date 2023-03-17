namespace Symbiotic.Actor
{
    public interface IFixUpdateInterface
    {
        void PreFixUpdate();
        void OnFixUpdate();
        void PostFixUpdate();
    }
}