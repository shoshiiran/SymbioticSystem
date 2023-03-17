using Symbiotic.Core;
using UnityEngine;
using VContainer;
public class simpleBehavior : Behavior
{
    [Inject]
    readonly ILog _log;


    // public simpleBehavior(BehaviorService behaviorService) : base(behaviorService)
    // {
    // }


    // public simpleBehavior()
    // {
    //     behaviorService.AddBehaviorManager(this);
    // }
    public override void OnDestroy()
    {
        throw new System.NotImplementedException();
    }

    public override void OnStart()
    {
        Debug.Log("simple Start");
    }

    public override void OnUpdate()
    {
        _log.Log("simple log update");
        // Debug.Log("simple log update");

    }
}