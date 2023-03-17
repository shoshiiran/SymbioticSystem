using UnityEngine;
namespace Symbiotic.Actor
{
    public static class GameObjectExtensions
    {
        public static ActorSysGroup AddActorSysGroup(this GameObject gameObject, ActorSysGroup sysGroup, WorldSystem worldSystem)
        {
            if (worldSystem == null) return null;

            var actorSysGroup = worldSystem.GetActorSysGroup(gameObject);
            if (actorSysGroup != null && actorSysGroup.GetType() == sysGroup.GetType())
            {
#if UNITY_EDITOR
                Debug.LogWarning("已经存在");
#endif
                return actorSysGroup;
            }

            #region sysGroup.BindActor(gameObject, worldSystem);

            sysGroup.GameObject = gameObject;

            foreach (var sys in sysGroup.GetActorAllSystems())
            {
                sys.GameObject = gameObject;
                worldSystem.AddSystem(sys as ActorSystem);
            }

            worldSystem.AddSysGroup(sysGroup);
            #endregion

            // sysGroup.BindActor(gameObject, worldSystem);


            return sysGroup;
        }

        public static ActorSysGroup AddActorSysGroup<T>(this GameObject gameObject, WorldSystem worldSystem) where T : ActorSysGroup, new()
        {
            if (worldSystem == null) return null;

            var actorSysGroup = worldSystem.GetActorSysGroup(gameObject);
            if (actorSysGroup != null && actorSysGroup.GetType() == typeof(T))
            {
#if UNITY_EDITOR
                Debug.LogWarning("已经存在");
#endif
                return actorSysGroup;
            }

            var sysGroup = new T();
            sysGroup.BindActor(gameObject, worldSystem);

            return sysGroup;
        }

        public static bool RemoveActorSysGroup(this GameObject gameObject, WorldSystem worldSystem)
        {
            if (worldSystem == null) return false;

            // WorldSystem.Instance.AddSystem(actorSystem);
            var actorSysGroup = worldSystem.GetActorSysGroup(gameObject);
            if (actorSysGroup == null)
            {
                return false;

            }
            actorSysGroup.UnbindActor(gameObject, worldSystem);

            return true;
        }



        public static T GetActorSysGroup<T>(this GameObject gameObject, WorldSystem worldSystem) where T : ActorSysGroup
        {
            if (worldSystem == null) return null;

            var actorSysGroup = worldSystem.GetActorSysGroup(gameObject);
            if (actorSysGroup == null) return null;

            return actorSysGroup as T;
        }

        public static bool HasActorSysGroup<T>(this GameObject gameObject, WorldSystem worldSystem) where T : ActorSysGroup
        {
            if (worldSystem == null) return false;

            var actorSysGroup = worldSystem.GetActorSysGroup(gameObject);
            if (actorSysGroup == null) return false;

            return actorSysGroup.GetType() == typeof(T);
        }

        public static void AddActorSystem(this GameObject gameObject, ActorSystem actorSystem, WorldSystem worldSystem)
        {
            if (worldSystem == null) return;

            actorSystem.GameObject = gameObject;
            worldSystem.AddSystem(actorSystem);
        }
        public static void AddActorSystem<T>(this GameObject gameObject, WorldSystem worldSystem) where T : ActorSystem, new()
        {
            if (worldSystem == null) return;

            var actorSystem = new T();
            actorSystem.GameObject = gameObject;
            worldSystem.AddSystem(actorSystem);
        }
        public static void RemoveActorSystem(this GameObject gameObject, ActorSystem actorSystem, WorldSystem worldSystem)
        {
            if (worldSystem == null) return;

            // WorldSystem.Instance.AddSystem(actorSystem);
            worldSystem.RemoveSystem(actorSystem);
        }
        public static T GetActorSystem<T>(this GameObject gameObject, WorldSystem worldSystem) where T : ActorSystem
        {
            if (worldSystem == null) return null;

            return worldSystem.GetSystem<T>(gameObject);
        }

        // public static bool HasActorSystem<T>(this GameObject gameObject, WorldSystem worldSystem) where T : ActorSystem
        // {
        //     if (worldSystem == null) return false;

        //     // return worldSystem.HasSystem<T>(gameObject);
        // }


    }
}