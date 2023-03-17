using VContainer;
using VContainer.Unity;
using Symbiotic.Core;
using Symbiotic.Actor;
using UnityEngine;

public class Launcher : IStartable, ITickable, IInitializable, IPostStartable, IFixedTickable, ILateTickable
{
    [Inject] private readonly ILog _log;
    [Inject] BehaviorService behaviorService;
    simpleBehavior simpleBehavior;

    WorldSystem worldSystem;


    public void FixedTick()
    {
        worldSystem.OnFixedUpdate();
    }

    public void Initialize()
    {
        // simpleBehavior = new simpleBehavior();
        worldSystem = new WorldSystem();
        var test = new Test();
        // worldSystem.AddActorSysGroup(go, test);

    }

    public void LateTick()
    {
        worldSystem.OnLateUpdate();
    }

    public void PostStart()
    {
        // simpleBehavior = new simpleBehavior();

    }

    public void Start()
    {
        _log.Log("Start -- Launcher");
        _log.Log(((LogService)_log).b.ToString());

        worldSystem.OnStart();

    }

    public void Tick()
    {
        // _log.Log("Tick -- Launcher");
        worldSystem.OnUpdate();
        
    }
}