using VContainer.Unity;
using Leopotam.EcsLite;
using System;

public class EcsService : IStartable, ITickable, IFixedTickable, ILateTickable, IDisposable
{
    EcsWorld _world;
    EcsSystems _systems;
    EcsSystems _lateSystems;

    EcsSystems _fixSystems;

    public void Start()
    {
        _world = new EcsWorld();
        _systems = new EcsSystems(_world);
        _lateSystems = new EcsSystems(_world);
        _fixSystems = new EcsSystems(_world);

        _systems.Init();
        _lateSystems.Init();
        _fixSystems.Init();
    }

    public void Tick()
    {
        _systems.Run();
    }

    public void FixedTick()
    {
        _fixSystems.Run();
    }

    public void LateTick()
    {
        _lateSystems.Run();
    }

    public void Dispose()
    {
        if (_systems != null)
        {
            _systems.Destroy();
            _systems = null;
        }
        if (_lateSystems != null)
        {
            _lateSystems.Destroy();
            _lateSystems = null;
        }
        if (_fixSystems != null)
        {
            _fixSystems.Destroy();
            _fixSystems = null;
        }
        if (_world != null)
        {
            _world.Destroy();
            _world = null;
        }
    }
}