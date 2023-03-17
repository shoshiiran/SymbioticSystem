using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Cysharp.Threading.Tasks;
namespace Symbiotic.Actor
{

    public class WorldSystem : IWorldSystem
    {


        // readonly HashSet<IInitInterface> _allInitSystems = new();

        readonly HashSet<IFixUpdateInterface> _allFixUpdateSystems = new();
        readonly HashSet<IUpdateInterface> _allUpdateSystems = new();
        readonly HashSet<ILateUpdateInterface> _allLateUpdateSystems = new();



        // readonly SortedSet<ActorSystem> _actorSystems = new();
        readonly Dictionary<int, ActorSysGroup> _actorSysGroup = new();


        Task[] taskList;
        TaskFactory factory = new TaskFactory();

        public CancellationTokenSource cts = new CancellationTokenSource();

        int _milliseconds;

        bool _updateTask = true;


        public void SetFPS(int fps)
        {
            fps = Mathf.Clamp(fps, 30, 100);
            _milliseconds = 1000 / fps;
        }



        public void AddSystem(ActorSystem actorSystem)
        {
            // AddActorSystem(actorSystem);
            if (actorSystem is IUpdateInterface)
            {
                _allUpdateSystems.Add(actorSystem as IUpdateInterface);
            }
            if (actorSystem is IFixUpdateInterface)
            {
                _allFixUpdateSystems.Add(actorSystem as IFixUpdateInterface);

            }
            if (actorSystem is ILateUpdateInterface)
            {
                _allLateUpdateSystems.Add(actorSystem as ILateUpdateInterface);
            }
        }

        public void RemoveSystem(ActorSystem actorSystem)
        {
            // AddActorSystem(actorSystem);
            if (actorSystem is IUpdateInterface)
            {
                _allUpdateSystems.Remove(actorSystem as IUpdateInterface);

            }
            if (actorSystem is IFixUpdateInterface)
            {
                _allFixUpdateSystems.Remove(actorSystem as IFixUpdateInterface);

            }
            if (actorSystem is ILateUpdateInterface)
            {
                _allLateUpdateSystems.Remove(actorSystem as ILateUpdateInterface);
            }
        }


        public T GetSystem<T>(GameObject gameObject) where T : ActorSystem
        {
            return GetSystem<T>(gameObject.GetHashCode());
        }

        public T GetSystem<T>(int hashCode) where T : ActorSystem
        {
            var actorSysGroup = GetActorSysGroup(hashCode);
            if (actorSysGroup == null) return null;
            foreach (var system in actorSysGroup.GetActorAllSystems())
            {
                if (system is T)
                {
                    return system as T;
                }
            }
            return null;
        }

        public T GetSystem<T>(string name) where T : ActorSystem
        {
            var actorSysGroup = GetActorSysGroup(name);
            if (actorSysGroup == null) return null;
            foreach (var system in actorSysGroup.GetActorAllSystems())
            {
                if (system is T)
                {
                    return system as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取第一个符合条件的system
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private T GetSystem<T>() where T : ActorSystem
        {
            foreach (var actorSysGroup in _actorSysGroup.Values)
            {
                foreach (var system in actorSysGroup.GetActorAllSystems())
                {
                    if (system is T)
                    {
                        return system as T;
                    }
                }
            }
            return null;
        }

        public ActorSysGroup GetActorSysGroup(GameObject gameObject)
        {
            return GetActorSysGroup(gameObject.GetHashCode());
        }

        public ActorSysGroup GetActorSysGroup(int hashCode)
        {
            _actorSysGroup.TryGetValue(hashCode, out var actorSysGroup);
            return actorSysGroup;
        }
        /// <summary>
        /// 获得第一个gameobject名字为name的ActorSysGroup
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActorSysGroup GetActorSysGroup(string name)
        {
            foreach (var actorSysGroup in _actorSysGroup.Values)
            {
                if (actorSysGroup.GameObject.name == name)
                {
                    return actorSysGroup;
                }
            }
            return null;
        }

        public void AddSysGroup(ActorSysGroup sysGroup)
        {
            if (_actorSysGroup.TryAdd(sysGroup.GameObject.GetHashCode(), sysGroup))
            {
#if UNITY_EDITOR
                Debug.Log("Actors集合添加" + sysGroup.GameObject.name + "成功,ActorSysGroup Name:" + sysGroup.ToString());
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Actors集合添加" + sysGroup.GameObject.name + "失败,ActorSysGroup Name:" + sysGroup.ToString());
#endif
            }
        }

        public void RemoveSysGroup(ActorSysGroup sysGroup)
        {
            if (_actorSysGroup.Remove(sysGroup.GameObject.GetHashCode()))
            {
#if UNITY_EDITOR
                Debug.Log("Actors集合移除" + sysGroup.GameObject.name + "成功,ActorSysGroup Name:" + sysGroup.ToString());
#endif
            }
            else
            {
#if UNITY_EDITOR
                Debug.LogError("Actors集合移除" + sysGroup.GameObject.name + "失败,ActorSysGroup Name:" + sysGroup.ToString());
#endif
            }
        }


        public void Run()
        {

        }

        public void OnDestroy()
        {
            // foreach (var sys in _allActorSystems)
            // {
            //     sys.OnDestroy();
            // }

            cts.Cancel();
        }

        public void OnPause()
        {
            // foreach (var sys in _allActorSystems)
            // {
            //     sys.OnPause();
            // }
        }

        public void OnResume()
        {
            // foreach (var sys in _allActorSystems)
            // {
            //     sys.OnResume();
            // }
        }

        public void OnStart()
        {
            #region task
            // new Task(_ =>
            //     {
            //         Debug.Log("fps" + _milliseconds);
            //         while (!cts.IsCancellationRequested)
            //         {
            //             if (_updateTask)
            //             {
            //                 _updateTask = false;
            //                 RunActorSystem();
            //                 Debug.Log($"ThreadID_Task Start={Thread.CurrentThread.ManagedThreadId.ToString()}");

            //             }

            //             Thread.Sleep(5000);
            //         }
            //     }, cts.Token, TaskCreationOptions.LongRunning).Start();


            // UniTask.Run(async () =>
            //     {
            //         Debug.Log("fps" + _milliseconds);
            //         while (!cts.IsCancellationRequested)
            //         {
            //             if (_updateTask)
            //             {
            //                 _updateTask = false;

            //                 Debug.Log($"ThreadID_Task Start={Thread.CurrentThread.ManagedThreadId.ToString()}");

            //                 RunActorSystem();

            //                 // foreach (var sys in _allSystems)
            //                 // {
            //                 //     sys.OnUpdate();
            //                 // }
            //             }

            //             await UniTask.Delay(1000);
            //         }
            //     });
            #endregion

            Debug.Log("CPU核心数量:" + SystemInfo.processorCount);

            Debug.Log($"ThreadID_Mono={Thread.CurrentThread.ManagedThreadId.ToString()}");

            // foreach (var sys in _allActorSystems)
            // {
            //     Debug.Log("worldSysytem Onstart执行");
            //     sys.OnStart();
            // }

        }
        public static async Task Method1()
        {
            await Task.Run(() =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                        }
                    });
        }


        //*Unitask版本
        private async UniTask RunActorSystem()
        {


            //UniTask.Run(Action, bool, CancellationToken)' is obsolete: 'UniTask.Run is similar as Task.Run, it uses ThreadPool. For equivalent behaviour, use UniTask.RunOnThreadPool instead. If you don't want to use ThreadPool, you can use UniTask.Void(async void) or UniTask.Create(async UniTask) too.' [Assembly-CSharp]csharp(CS0618)


            //todo 所有的actor 先init完成结束
            //todo 2 所有的actor 执行 update
            UniTask.Void(async () =>
            {
                // //todo actor 1.preupdate  ->2.update -->3.lateupdate
                Debug.Log($"ThreadID_Task1={Thread.CurrentThread.ManagedThreadId.ToString()}");
                // Thread.Sleep(1000);

                // foreach (var sys in _allActorSystems)
                // {
                //     // sys.OnUpdate();
                // }
                _updateTask = true;
                await UniTask.Yield();
            });
            // var task2 = new Task(_ =>
            // {
            //     Debug.Log($"ThreadID_Task2={Thread.CurrentThread.ManagedThreadId.ToString()}");
            //     // Thread.Sleep(5000);
            // }, cts.Token);
            // task2.Start();

            // taskList = new Task[]{
            //     task1,
            //     task2,
            //     };


            // factory.ContinueWhenAll(taskList, _ =>
            //     {
            //         Debug.Log("子线程完毕" + System.DateTime.Now.Second);
            //         _updateTask = true;
            //     }, cts.Token);
        }


        //*task 版本
        // private void RunActorSystem()
        // {

        //     //todo 所有的actor 先init完成结束
        //     //todo 2 所有的actor 执行 update
        //     var task1 = new Task(t =>
        //     {
        //         //todo actor 1.preupdate  ->2.update -->3.lateupdate
        //         Debug.Log($"ThreadID_Task1={Thread.CurrentThread.ManagedThreadId.ToString()}");
        //         // Thread.Sleep(1000);

        //         foreach (var sys in _allSystems)
        //         {
        //             sys.OnUpdate();
        //         }
        //     }, cts.Token);
        //     task1.Start();
        //     var task2 = new Task(_ =>
        //     {
        //         Debug.Log($"ThreadID_Task2={Thread.CurrentThread.ManagedThreadId.ToString()}");
        //         // Thread.Sleep(5000);
        //     }, cts.Token);
        //     task2.Start();

        //     taskList = new Task[]{
        //         task1,
        //         task2,
        //         };


        //     factory.ContinueWhenAll(taskList, _ =>
        //         {
        //             Debug.Log("子线程完毕" + System.DateTime.Now.Second);
        //             _updateTask = true;
        //         }, cts.Token);
        // }

        public void OnUpdate()
        {

            // await UniTask.Yield();

            foreach (var sys in _allUpdateSystems)
            {
                sys.PreUpdate();
            }

            foreach (var sys in _allUpdateSystems)
            {
                sys.OnUpdate();
            }
            foreach (var sys in _allUpdateSystems)
            {
                sys.PostUpdate();
            }
        }

        public void OnFixedUpdate()
        {
            foreach (var sys in _allFixUpdateSystems)
            {
                sys.PreFixUpdate();
            }

            foreach (var sys in _allFixUpdateSystems)
            {
                sys.OnFixUpdate();
            }
            foreach (var sys in _allFixUpdateSystems)
            {
                sys.PostFixUpdate();
            }

        }

        public void OnLateUpdate()
        {
            foreach (var sys in _allLateUpdateSystems)
            {
                sys.PreLateUpdate();
            }

            foreach (var sys in _allLateUpdateSystems)
            {
                sys.OnLateUpdate();
            }
            foreach (var sys in _allLateUpdateSystems)
            {
                sys.PostLateUpdate();
            }
        }

        public void AddSystem(IActorInterface system)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSystem(IActorInterface system)
        {
            throw new System.NotImplementedException();
        }
    }
}