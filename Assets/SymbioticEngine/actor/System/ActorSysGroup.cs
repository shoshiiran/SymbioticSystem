using System.Collections.Generic;
using UnityEngine;

namespace Symbiotic.Actor
{
    public abstract class ActorSysGroup
    {
        readonly HashSet<IActorInterface> _actorAllSystems = new();
        readonly Dictionary<int, IActorInterface> _actorSystems = new();
        public GameObject GameObject { get; set; }

        bool Enabled
        {
            get => GameObject.activeSelf;
        }

        public ActorSysGroup()
        {
            InitBehavior();
            Debug.Log("ActorSysGroup");
        }

        //TODO worldsystem 耦合度太高，需要解耦
        public void BindActor(GameObject go, WorldSystem worldSystem)
        {
            GameObject = go;
            InitBehavior();

            // foreach (var sys in _actorAllSystems)
            // {
            //     sys.GameObject = GameObject;
            //     worldSystem.AddSystem(sys as ActorSystem);
            // }

            // worldSystem.AddSysGroup(this);
        }

        public void UnbindActor(GameObject go, WorldSystem worldSystem)
        {
            if (GameObject == null) return;

            foreach (var sys in _actorAllSystems)
            {
                sys.GameObject = GameObject;
                worldSystem.RemoveSystem(sys as ActorSystem);
            }

            worldSystem.RemoveSysGroup(this);
            GameObject = null;

        }


        /// <summary>
        /// 初始化，添加system
        /// </summary>
        public abstract void InitBehavior();


        /// <summary>
        /// 获取actorBehaviours注册的所有system
        /// </summary>
        /// <returns></returns>
        public HashSet<IActorInterface> GetActorAllSystems()
        {
            return _actorAllSystems;
        }

        public void AddGroupSystem(IActorInterface system)
        {
            _actorAllSystems.Add(system);
        }

        public void RemoveSystem(IActorInterface system)
        {
            _actorAllSystems.Remove(system);
        }


    }
}