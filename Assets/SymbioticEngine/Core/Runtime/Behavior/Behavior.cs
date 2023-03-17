using VContainer;
namespace Symbiotic.Core
{
    public abstract class Behavior : IBehavior
    {
        [Inject] readonly BehaviorService _behaviorService;
        public Behavior()
        {
            _behaviorService.AddBehaviorManager(this);
        }
        // public Behavior(BehaviorService behaviorService)
        // {
        //     behaviorService.AddBehaviorManager(this);
        // }

        public abstract void OnStart();

        public abstract void OnUpdate();
        public abstract void OnDestroy();
    }
}