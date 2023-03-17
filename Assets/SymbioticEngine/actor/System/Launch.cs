using UnityEngine;
using Symbiotic.Actor;

public class Launch : MonoBehaviour
{
    WorldSystem worldSystem;
    public GameObject Object;

    private GameObject go;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // worldSystem = new WorldSystem();
        // worldSystem.SetFPS(30);
        // worldSystem.OnStart();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        // Instantiate(gameObject).AddActorMono(new Test());
        Debug.Log("开始--生成gameobject" + System.DateTime.Now);

        go = Instantiate(Object, Vector3.zero, Quaternion.identity);

        Debug.Log("完成---生成gameobject" + System.DateTime.Now);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        // worldSystem.OnUpdate();
        if (Input.GetKeyDown(KeyCode.A))
        {
            // go.AddActorSysGroup(new Test(), worldSystem);
            // Debug.Log(go.HasActorSysGroup<Test>(worldSystem));
            // go.RemoveActorSysGroup(worldSystem);

            // go.AddActorSysGroup<Test>(worldSystem);
            // go.GetActorSysGroup<Test>(worldSystem);

            // worldSystem.AddActorSysGroup(go, new Test());
            // worldSystem.AddActorSysGroup<Test>(go);
            var test = new Test();
            worldSystem.AddActorSysGroup(go, test);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            go.RemoveActorSysGroup(worldSystem);
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // for (int i = 0; i < 100; i++)
            // {
            //     Instantiate(Object, Random.insideUnitSphere * 3, Quaternion.identity).AddActorMono(new Test(), worldSystem);

            // }


            // worldSystem.cts.Cancel();
        }

        // if (Input.GetKeyDown(KeyCode.Mouse0))
        // {
        //     for (int i = 0; i < 100; i++)
        //     {
        //         Instantiate(Object, Random.insideUnitSphere * 3, Quaternion.identity);

        //     }

        // }
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        // worldSystem.OnDestroy();
    }
}