
using System.Collections.Generic;
using UnityEngine;

struct ObjectPoolComponent
{
    public GameObject Prefab;
    public int InitialSize;
    public int MaxSize;
    public int TargetActiveCount;
    public int ActiveCount;
    public Queue<GameObject> Pool;
}