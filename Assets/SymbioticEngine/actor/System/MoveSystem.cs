using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using System.Threading;
namespace Symbiotic.Actor
{


    public class MoveSystem : ActorSystem, IUpdateInterface
    {
        public override void OnDestroy()
        {
            Debug.Log("moveSystem Destroy");

        }

        public override void OnStart()
        {
            Debug.Log("moveSystem start");
        }

        //This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread. [Assembly-CSharp]
        public void OnUpdate()
        {
            // Debug.Log("开始 move");
            // Debug.Log(Actor.transform);
            GameObject.transform.Translate(Vector3.forward, Space.World);
        }

        public void PostUpdate()
        {
        }

        public void PreUpdate()
        {
        }
    }
}