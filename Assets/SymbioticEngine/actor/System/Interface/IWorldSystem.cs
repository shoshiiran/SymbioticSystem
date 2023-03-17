namespace Symbiotic.Actor
{


    // IWorldSystem.cs
    public interface IWorldSystem
    {
        void AddSystem(IActorInterface system);
        void RemoveSystem(IActorInterface system);
        void AddSysGroup(ActorSysGroup sysGroup);
        void RemoveSysGroup(ActorSysGroup sysGroup);
    }
}