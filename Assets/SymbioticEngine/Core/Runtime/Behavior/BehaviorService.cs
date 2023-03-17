using UnityEngine;
using VContainer;
using VContainer.Unity;
using System.Collections.Generic;

namespace Symbiotic.Core
{
    public class BehaviorService : ITickable, IStartable, IInitializable
    {
        private List<Behavior> _behaviors;

        public void AddBehaviorManager(Behavior behavior)
        {
            _behaviors.Add(behavior);
        }

        public void Tick()
        {
            for (int i = _behaviors.Count - 1; i >= 0; i--)
            {
                _behaviors[i].OnUpdate();
            }

        }


        public void Start()
        {

            foreach (var behavior in _behaviors)
            {
                behavior.OnStart();
                Debug.Log(_behaviors.Count);
            }
            Debug.Log("Behaviors");
        }

        public void Initialize()
        {
            _behaviors = new List<Behavior>();
        }
    }
}