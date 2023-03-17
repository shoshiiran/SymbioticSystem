using Cysharp.Threading.Tasks;
namespace Symbiotic.Actor
{
    public abstract class TaskSystem : IInitInterface
    {
        public abstract void OnDestroy();

        public abstract void OnInit(IWorldSystem system);


        public virtual void OnPause()
        {

        }

        public virtual void OnResume()
        {

        }
    }
}