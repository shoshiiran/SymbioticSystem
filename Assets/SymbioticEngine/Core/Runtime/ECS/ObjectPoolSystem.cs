using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

class ObjectPoolSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsWorld _world;

    public void Init(IEcsSystems systems)
    {
        _world = systems.GetWorld();

        // 创建一个新的实体并添加ObjectPoolComponent组件
        // CreateObjectPoolEntity(_world, prefab, initialSize, maxSize, targetActiveCount);
    }

    public void CreateObjectPoolEntity(EcsWorld world, GameObject prefab, int initialSize, int maxSize, int targetActiveCount)
    {
        var entity = world.NewEntity();
        EcsPool<ObjectPoolComponent> objectPoolComponentPool = world.GetPool<ObjectPoolComponent>();
        ref var objectPoolComponent = ref objectPoolComponentPool.Add(entity);
        objectPoolComponent.Prefab = prefab;
        objectPoolComponent.InitialSize = initialSize;
        objectPoolComponent.MaxSize = maxSize;
        objectPoolComponent.TargetActiveCount = targetActiveCount;
        objectPoolComponent.ActiveCount = 0;
    }

    public void Run(IEcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        var filter = world.Filter<ObjectPoolComponent>().End();
        var objectPool = world.GetPool<ObjectPoolComponent>();
        // 遍历所有需要处理的实体
        foreach (var entity in filter)
        {
            // 获取对象池组件
            ref ObjectPoolComponent obj = ref objectPool.Get(entity);

            // 如果对象池为空，则初始化
            if (obj.Pool == null)
            {
                InitializeObjectPool(obj);
            }

            // 处理对象池中的对象
            ProcessObjectPool(obj);
        }
    }


    private void InitializeObjectPool(ObjectPoolComponent objectPool)
    {
        // 创建对象池并存储在组件中
        objectPool.Pool = new Queue<GameObject>();

        // 根据预设和初始大小填充对象池
        for (int i = 0; i < objectPool.InitialSize; i++)
        {
            var obj = Object.Instantiate(objectPool.Prefab);
            obj.SetActive(false);
            objectPool.Pool.Enqueue(obj);
        }
    }

    private void ProcessObjectPool(ObjectPoolComponent objectPool)
    {
        // 根据需要激活或停用对象池中的对象
        while (objectPool.ActiveCount < objectPool.TargetActiveCount && objectPool.Pool.Count > 0)
        {
            var obj = objectPool.Pool.Dequeue();
            obj.SetActive(true);
            objectPool.ActiveCount++;
        }

        while (objectPool.ActiveCount > objectPool.TargetActiveCount && objectPool.Pool.Count < objectPool.MaxSize)
        {
            var obj = objectPool.Pool.Dequeue();
            obj.SetActive(false);
            objectPool.ActiveCount--;
        }
    }
}