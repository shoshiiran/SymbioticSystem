namespace Symbiotic.Core
{

    public interface IInit
    {
        /// <summary>
        /// 初始化行为
        /// </summary>
        void OnInit();
    }
    public interface IStart
    {
        /// <summary>
        /// 行为准备工作
        /// </summary>
        void OnStart();

    }

    public interface IUpdate
    {
        /// <summary>
        /// 更新行为
        /// </summary>
        void OnUpdate();
    }

    public interface IFixUpdate
    {
        /// <summary>
        /// 更新行为
        /// </summary>
        void OnFixUpdate();
    }
    public interface ILateUpdate
    {
        /// <summary>
        /// 更新行为
        /// </summary>
        void OnLateUpdate();
    }

    public interface IDestroy
    {
        /// <summary>
        /// 终结行为
        /// </summary>
        void OnDestroy();
    }


    /// <summary>
    /// 行为接口
    /// </summary>
    public interface IBehavior : IStart, IUpdate, IDestroy
    {


        // /// <summary>
        // /// 暂停行为
        // /// </summary>
        // void OnPause();
        // /// <summary>
        // /// 恢复行为
        // /// </summary>
        // void OnResume();
    }
}