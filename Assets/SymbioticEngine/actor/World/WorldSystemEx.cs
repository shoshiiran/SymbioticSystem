using UnityEngine;
namespace Symbiotic.Actor
{
    public static class WorldSystemEx
    {

        public static ActorSysGroup GetActorBehaviour(this WorldSystem worldSystem, GameObject gameObject)
        {
            return worldSystem.GetActorSysGroup(gameObject);
        }

        public static ActorSysGroup AddActorSysGroup(this WorldSystem worldSystem, GameObject gameObject, ActorSysGroup sysGroup)
        {
            return gameObject.AddActorSysGroup(sysGroup, worldSystem);
        }
        public static ActorSysGroup AddActorSysGroup<T>(this WorldSystem worldSystem, GameObject gameObject) where T : ActorSysGroup, new()
        {
            return gameObject.AddActorSysGroup<T>(worldSystem);
        }
    }
}