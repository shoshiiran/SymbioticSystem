namespace Symbiotic.Actor
{
    public interface ILateUpdateInterface
    {
        void PreLateUpdate();
        void OnLateUpdate();
        void PostLateUpdate();
    }
}