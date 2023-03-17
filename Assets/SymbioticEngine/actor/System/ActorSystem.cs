using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Symbiotic.Actor
{
    public abstract class ActorSystem : IActorInterface
    {
        //TODO  unity gameobject 调用destroy时，将 actorsystem 的解绑移出 worldsystem，回收进池。
        //TODO gameobject bind 时，先查看池内有无空闲的system，重新绑定。
        public GameObject GameObject { get; set; }

        public bool Enabled
        {
            get
            {
                if (GameObject == null) return false;

                return GameObject.activeSelf;
            }
            set
            {
                if (GameObject != null)
                {
                    GameObject.SetActive(value);
                }
            }
        }


        public ActorSystem()
        {
            Debug.Log("ActorSystem");

            OnStart();

        }

        public abstract void OnStart();
        public virtual void OnDestroy()
        {
            Debug.Log("Destory ActorSystem");
            if (GameObject != null)
            {
                Enabled = false;
                GameObject.Destroy(GameObject);
            }
        }

        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }


        ~ActorSystem()
        {
            if (GameObject != null)
            {
                Enabled = false;
                GameObject.Destroy(GameObject);
            }
        }
    }
}