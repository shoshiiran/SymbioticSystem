using UnityEngine;
using Symbiotic.Actor;
public class Test : ActorSysGroup
{
    public override void InitBehavior()
    {
        Debug.Log("开始 --添加 moveSystem" + System.DateTime.Now);
        AddGroupSystem(new MoveSystem());
        Debug.Log("完成 --添加 moveSystem" + System.DateTime.Now);
    }
}